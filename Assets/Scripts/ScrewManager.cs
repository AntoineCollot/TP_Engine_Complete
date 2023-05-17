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
                //Désactive l'object touché (on pourrait vérifier que c'est bien une vis).
                hit.transform.gameObject.SetActive(false);
            }

            bool allScrewsDisabledState = AreAllScrewsDisabled();
            //On vérifie que l'état vient de changer (différent de l'état précédent) et que les vis sont désactivées.
            if (allScrewsDisabledState && lastAllScrewDisabledState != allScrewsDisabledState)
            {
                onAllScrewsDisabled.Invoke();
            }

            //Met à jour l'état précédant pour la prochaine fois.
            lastAllScrewDisabledState = allScrewsDisabledState;
        }
    }

    /// <summary>
    /// Vérifie si toutes les vis sont désactivées.
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
