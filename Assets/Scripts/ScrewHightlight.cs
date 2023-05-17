using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ScrewHightlight : MonoBehaviour
{
    public Material mat;
    public float transitionTime = 0.5f;
    public bool isHighlighted { get; private set; }

    private void Start()
    {
        //Initialise l'état highlighté
        isHighlighted = mat.GetFloat("_Highlight")>0.5f;
    }

    IEnumerator HighlightAnim(float end, float time)
    {
        float t = 0;
        float start = mat.GetFloat("_Highlight");
        while(t<1)
        {
            t += Time.deltaTime / time;

            mat.SetFloat("_Highlight", Mathf.Lerp(start, end, t));

            yield return null;
        }
    }

    public void SetHightlight(bool value)
    {
        isHighlighted = value;
        if (value)
            StartCoroutine(HighlightAnim(1, transitionTime));
        else
            StartCoroutine(HighlightAnim(0, transitionTime));
    }

    public void ToggleHighlight()
    {
        SetHightlight(!isHighlighted);
    }
}
