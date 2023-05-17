using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float rotationSpeed = 30;
    public float translationSpeed = 0.5f;
    public float maxTranslationDistance = 2;
    Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        //Pivoter la camera avec les fléches
        transform.Rotate(Vector3.up * -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        //Déplacer la camera avec les fléches
        cam.transform.Translate(Vector3.up * translationSpeed * Time.deltaTime * Input.GetAxis("Vertical"), Space.World);

        //Limiter la translation max
        Vector3 camPos = cam.transform.position;
        camPos.y = Mathf.Clamp(camPos.y, -maxTranslationDistance, maxTranslationDistance);
        cam.transform.position = camPos;

        //Regarde le pivot
        cam.transform.LookAt(transform.position);
    }
}
