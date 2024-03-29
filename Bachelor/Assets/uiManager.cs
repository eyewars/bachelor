using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiManager : MonoBehaviour{
    public TextMeshProUGUI batteryText;
    
    void Start() { }

    void Update() {
        batteryText.SetText("Battery: " + ((playerManager.instance.battery / playerManager.instance.maxBattery) * 100).ToString("N0") + "%");
    }
}