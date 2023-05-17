using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour
{
    public Transform seeThroughSpherePrefab;
    public LayerMask mask;
    Camera cam;
    Transform spawnedSphere;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        //Détecter un clique gauche
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Ray depuis la souris
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //Raycast
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
            {
                //Si une sphere existe
                if (spawnedSphere != null)
                {
                    //On la détruit
                    Destroy(spawnedSphere.gameObject);
                    spawnedSphere = null;
                }
                //Si il n'y a pas de sphère
                else
                {
                    //Spawn
                    spawnedSphere = Instantiate(seeThroughSpherePrefab, transform);
                    spawnedSphere.position = hit.point;
                }
            }
        }
    }
}
