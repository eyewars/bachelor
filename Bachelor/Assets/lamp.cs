using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class lamp : MonoBehaviour{
    public bool isStrong;

    private float strongIntesity = 200f;
    private float weakIntensity = 2f;

    // MÅ BYTTE TYPE TIL NOE HDRP LYS GREIER 
    // https://forum.unity.com/threads/light-intensity-doesnt-work-with-hdrp.706382/
    private Light myLight;
    
    // Mathf.PingPong() kan være nice for noe, feks alarm greier på starten
    void Start() {
        myLight = transform.GetChild(1).GetComponent<Light>();

        if (isStrong) {
            myLight.intensity = strongIntesity;
            Debug.Log("STERK!!!!!!!!!!");
        } else {
            myLight.intensity = weakIntensity;
            Debug.Log("svak");
        }
    }

    void Update() { }
}