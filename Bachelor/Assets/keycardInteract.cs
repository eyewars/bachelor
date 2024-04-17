using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycardInteract : MonoBehaviour, interactable{
    public int keyId;

    public void interact() {
        playerManager.instance.keyIds.Add(keyId);
        
        Destroy(gameObject);
    }
}