using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSplitting : MonoBehaviour
{
    //Optionel, une struct permet de mieux ranger ces variables
    [System.Serializable]
    public struct EnginePart
    {
        public Transform enginePart;
        [HideInInspector] public Vector3 originalPosition;
        public Transform splitTargetPos;
    }

    public float splitAnimationTime = 1;
    //Tableau de la struct contenant les variables nescessaires au split
    public EnginePart[] parts;

    void Start()
    {
        FindObjectOfType<ScrewManager>().onAllScrewsDisabled.AddListener(OnAllScrewsDisabled);

        //Initialise les positions d'origines
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].originalPosition = parts[i].enginePart.position;
        }
    }

    /// <summary>
    /// Callback de l'événement des vis.
    /// </summary>
    private void OnAllScrewsDisabled()
    {
        //Met fin aux eventuelles autres coroutines si jamais l'utilisateur clique trop vite et essaie de lancer plusieurs animations à la fois
        StopAllCoroutines();
        StartCoroutine(SplitEngine(0,1,splitAnimationTime));
    }

    public void UnsplitEngine()
    {
        StopAllCoroutines();
        StartCoroutine(SplitEngine(1, 0, splitAnimationTime));
    }

    /// <summary>
    /// Animation de split du moteur.
    /// Start et end permettent de unsplit le moteur si besoin, en commencant de 1 et finissant à 0 (0 il est fermé, 1 il est ouvert).
    /// </summary>
    IEnumerator SplitEngine(float start, float end, float time)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;

            foreach (EnginePart part in parts)
            {
                //On utilise la fonction SmoothStep pour smoother t (courbe ease in out au lieu de linéaire)
                float smoothedT = Mathf.SmoothStep(start, end, t);
                part.enginePart.position = Vector3.Lerp(part.originalPosition, part.splitTargetPos.position, smoothedT);
            }

            yield return null;
        }
    }
}
