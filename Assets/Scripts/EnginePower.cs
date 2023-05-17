using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePower : MonoBehaviour
{
    [Range(0,1)] public float power;

    [Header("Rotor")]
    public float maxRotorSpeed = 360;
    public Transform rotor;

    [Header("Exhaust")]
    public Material exhaustMaterial;

    void Update()
    {
        //Rotate the rotor
        rotor.Rotate(Vector3.forward * power * maxRotorSpeed * Time.deltaTime, Space.Self);
        //Update the material
        exhaustMaterial.SetFloat("_Heat", power);
    }

    public void SetPower(float power)
    {
        power = Mathf.Clamp01(power);
        this.power = power;
    }
}
