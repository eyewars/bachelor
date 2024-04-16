using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryInteract : MonoBehaviour, interactable{
    public float batteryRefill;

    public void interact() {
        playerManager.instance.battery += batteryRefill;

        if (playerManager.instance.battery > playerManager.instance.maxBattery) {
            playerManager.instance.battery = playerManager.instance.maxBattery;
        }
        
        Destroy(gameObject);
    }
}