using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSplitting : MonoBehaviour
{
    [System.Serializable]
    public struct EnginePart
    {
        public Transform enginePart;
        [HideInInspector] public Vector3 originalPosition;
        public Transform splitTargetPos;
    }

    public float splitAnimationTime = 1;
    public EnginePart[] parts;

    void Start()
    {
        FindObjectOfType<ScrewManager>().onAllScrewsDisabled.AddListener(OnAllScrewsDisabled);
        
        //Initialise les positions d'origines
        for(int i=0; i<parts.Length; i++)
        {
            parts[i].originalPosition = parts[i].enginePart.position;
        }
    }

    private void OnAllScrewsDisabled()
    {
        StartCoroutine(SplitEngine(splitAnimationTime));
    }

    IEnumerator SplitEngine(float time)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;

            foreach (EnginePart part in parts)
            {
                float smoothedT = Mathf.SmoothStep(0.0f, 1.0f, t);
                part.enginePart.position = Vector3.Lerp(part.originalPosition, part.splitTargetPos.position, smoothedT);
            }

            yield return null;
        }
    }

    /// <summary>
    /// Optionnel, permet de dessiner des lignes dans la scène pour aider au debug
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (parts == null)
            return;

        Gizmos.color = Color.blue;
        foreach (EnginePart part in parts)
        {
            //Make sure the target pos & part is not null
            if (part.enginePart == null || part.splitTargetPos == null)
                continue;

            //If the original position isn't set, draw from the object to the target
            if (part.originalPosition == Vector3.zero)
                Gizmos.DrawLine(part.enginePart.position, part.splitTargetPos.position);
            //If the original position is set, draw from the original pos to the target
            else
                Gizmos.DrawLine(part.originalPosition, part.splitTargetPos.position);
        }
    }
}
