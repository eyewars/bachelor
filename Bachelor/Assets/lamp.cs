using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class lamp : MonoBehaviour{
    public bool isStrong;

    private float strongIntesity = 100f;
    private float weakIntensity = 1f;

    public Material strongEmission;
    public Material weakEmission;

    private Material materialRenderer;

    // MÅ BYTTE TYPE TIL NOE HDRP LYS GREIER 
    // https://forum.unity.com/threads/light-intensity-doesnt-work-with-hdrp.706382/
    private HDAdditionalLightData myLight;

    // Mathf.PingPong() kan være nice for noe, feks alarm greier på starten
    void Start() {
        myLight = transform.GetChild(1).GetComponent<HDAdditionalLightData>();

        materialRenderer = GetComponent<Renderer>().material;

        changeIntensity();
    }

    public void toogleLamp() {
        if (isStrong) {
            isStrong = false;
        } else {
            isStrong = true;
        }

        changeIntensity();
    }

    private void changeIntensity() {
        if (isStrong) {
            myLight.intensity = strongIntesity;
            transform.GetChild(0).GetComponent<Renderer>().material = strongEmission;
        } else {
            myLight.intensity = weakIntensity;
            transform.GetChild(0).GetComponent<Renderer>().material = weakEmission;
        }
    }
}