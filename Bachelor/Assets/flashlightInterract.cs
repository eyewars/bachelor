using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightInterract : MonoBehaviour, interactable{
    public void interact() {
        playerManager.instance.hasFlashlight = true;
        
        Destroy(gameObject);
    }
}