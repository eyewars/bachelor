using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour{
    private Light myLight;
    
    private AudioSource source;
    public  AudioClip[] interactSounds;

    void Start() {
        myLight = GetComponent<Light>();
        
        source = GetComponent<AudioSource>();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && playerManager.instance.hasFlashlight && (playerManager.instance.battery > 0)) {
            if (myLight.enabled) {
                myLight.enabled = false;
                source.clip = interactSounds[0];
            } else {
                myLight.enabled = true;
                source.clip = interactSounds[1];
            }
            
            source.Play();
        }

        if (myLight.enabled) {
            playerManager.instance.battery -= 1 * Time.deltaTime;

            if (playerManager.instance.battery <= 0) {
                myLight.enabled = false;
                playerManager.instance.battery = 0;
                source.clip = interactSounds[0];
                source.Play();
            }
        }
    }
}