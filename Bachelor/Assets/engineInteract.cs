using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class engineInteract : MonoBehaviour, IInteractableStart{
    public Light pointLight;

    public string HoverText
    {
        get
        {
            if (playerManager.instance.machineIds.Count == 1)
            {
                return "Deliver one machine part";
            }
            if(playerManager.instance.machineIds.Count > 1)
            {
                return $"Deliver {playerManager.instance.machineIds.Count} machine parts";
            }

            if (playerManager.instance.machineIdsUsed.Count == 0)
            {
                return "Need machine parts to repair";
            }

            if (playerManager.instance.machineIdsUsed.Count > 0 && playerManager.instance.machineIdsUsed.Count < 5)
            {
                return "Need more machine parts to repair";
            }

            return "Engine repaired!";
        }
    }

    void Start(){
        pointLight.color = new Color(0.789f, 0.035f, 0.035f);
    }

    public void InteractStart(){
        playerManager.instance.machineIdsUsed.AddRange(playerManager.instance.machineIds);
        playerManager.instance.machineIds.Clear();

        if (playerManager.instance.machineIdsUsed.Count == 5){
            playerManager.instance.hasWon = true;
            playerManager.instance.currentAudio = "winGame";
            playerManager.instance.currentClip = 0;
            playerManager.instance.shouldStopClip = true;
            pointLight.color = new Color(0.035f, 0.789f, 0.347f);
            
            playerManager.instance.Invoke("winSceneChange", 26f);
        }
    }
}
