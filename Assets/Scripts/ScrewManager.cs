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
            //Désactive l'object touché (on pourrait vérifier que c'est bien une vis).
            hit.transform.gameObject.SetActive(false);
        }

        bool allScrewsDisabledState = AreAllScrewsDisabled();
        //On vérifie que l'état vient de changer (différent de l'état précédent) et que les vis sont désactivées.
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
