using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScrewManager : MonoBehaviour
{
    Camera cam;

    public GameObject[] screws;
    public LayerMask screwLayer;
    bool lastAllScrewDisabledState;

    public UnityEvent onAllScrewsDisabled = new UnityEvent();

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //Raycast depuis la souris
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, screwLayer))
            {
                //D�sactive l'object touch� (on pourrait v�rifier que c'est bien une vis).
                hit.transform.gameObject.SetActive(false);
            }

            bool allScrewsDisabledState = AreAllScrewsDisabled();
            //On v�rifie que l'�tat vient de changer (diff�rent de l'�tat pr�c�dent) et que les vis sont d�sactiv�es.
            if (allScrewsDisabledState && lastAllScrewDisabledState != allScrewsDisabledState)
            {
                onAllScrewsDisabled.Invoke();
            }

            //Met � jour l'�tat pr�c�dant pour la prochaine fois.
            lastAllScrewDisabledState = allScrewsDisabledState;
        }
    }

    /// <summary>
    /// V�rifie si toutes les vis sont d�sactiv�es.
    /// </summary>
    /// <returns></returns>
    bool AreAllScrewsDisabled()
    {
        foreach (GameObject screw in screws)
        {
            if (screw.activeSelf)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Active toutes les vis
    /// </summary>
    public void EnableAllScrews()
    {
        foreach (GameObject screw in screws)
        {
            screw.SetActive(true);
        }
    }
}
