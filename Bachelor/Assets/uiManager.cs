using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class uiManager : MonoBehaviour{
    public TextMeshProUGUI batteryText;

    public Image battery;

    public Sprite[] batteryImages;
    
    void Start() { 
        battery.enabled = false;
    }

    void Update() {
        if (playerManager.instance.battery == playerManager.instance.maxBattery){
            battery.sprite = batteryImages[5];
        } else if (playerManager.instance.battery >= (playerManager.instance.maxBattery * 0.8f)){
            battery.sprite = batteryImages[4];
        } else if (playerManager.instance.battery >= (playerManager.instance.maxBattery * 0.6f)){
            battery.sprite = batteryImages[3];
        } else if (playerManager.instance.battery >= (playerManager.instance.maxBattery * 0.4f)){
            battery.sprite = batteryImages[2];
        } else if (playerManager.instance.battery >= (playerManager.instance.maxBattery * 0.2f)){
            battery.sprite = batteryImages[1];
        } else if (playerManager.instance.battery == 0){
            battery.sprite = batteryImages[0];
        }
    }
}