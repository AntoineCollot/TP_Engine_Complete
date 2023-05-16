using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScrewManager : MonoBehaviour
{
    Camera cam;
    public UnityEvent onAllScrewsDisabled = new UnityEvent();

    [Header("Screws")]
    public GameObject[] screws;
    public LayerMask screwLayer;
    bool lastAllScrewDisabledState;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //Raycast depuis la souris
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, screwLayer))
        {
            //D�sactive l'object touch� (on pourrait v�rifier que c'est bien une vis).
            hit.transform.gameObject.SetActive(false);
        }

        bool allScrewsDisabledState = AreAllScrewsDisabled();
        //On v�rifie que l'�tat vient de changer (diff�rent de l'�tat pr�c�dent) et que les vis sont d�sactiv�es.
        if(allScrewsDisabledState && lastAllScrewDisabledState!=allScrewsDisabledState)
        {
            onAllScrewsDisabled.Invoke();
        }

        lastAllScrewDisabledState = allScrewsDisabledState;
    }

    bool AreAllScrewsDisabled()
    {
        foreach (GameObject screw in screws)
        {
            if (screw.activeSelf)
                return false;
        }

        return true;
    }
}
