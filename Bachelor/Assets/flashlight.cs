using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour{
    public Transform placement;

    private Light myLight;
    
    void Start() {
        myLight = GetComponent<Light>();
    }
    void Update() {
        transform.rotation = placement.rotation;
        //transform.position = placement.position;

        if (Input.GetKeyDown(KeyCode.E) && playerManager.instance.hasFlashlight && (playerManager.instance.battery > 0)) {
            if (myLight.enabled) {
                myLight.enabled = false;
            } else {
                myLight.enabled = true;
            }
        }

        if (myLight.enabled) {
            playerManager.instance.battery -= 1 * Time.deltaTime;

            if (playerManager.instance.battery <= 0) {
                myLight.enabled = false;
                playerManager.instance.battery = 0;
            }
        }
    }
}