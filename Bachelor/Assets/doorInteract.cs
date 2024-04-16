using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorInteract : MonoBehaviour, interactable{
    public bool isUnlocked = false;
    public bool needsKey = false;
    public int keyId;
    public void interact() {
        if (needsKey) {
            if (playerManager.instance.keyIds.Contains(keyId)) {
                unlockDoor();
            }
        } else {
            unlockDoor();
        }
    }

    public void unlockDoor() {
        isUnlocked = true;
    }
}