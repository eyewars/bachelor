using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class flashlightInterract : MonoBehaviour, interactable{
    public TextMeshProUGUI batteryText;
    
    public void interact() {
        playerManager.instance.hasFlashlight = true;
        batteryText.enabled = true;
        
        Destroy(gameObject);
    }
}