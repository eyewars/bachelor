using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineInteract : MonoBehaviour, interactable{
    public Light pointLight;

    void Start(){
        pointLight.color = new Color(0.789f, 0.035f, 0.035f);
    }

    public void interact(){
        for (int i = 0; i < playerManager.instance.machineIds.Count; i++){
            playerManager.instance.machineIdsUsed.Add(playerManager.instance.machineIds[i]);
        }

        playerManager.instance.machineIds = new List<int>();

        if (playerManager.instance.machineIdsUsed.Count == 5){
            playerManager.instance.hasWon = true;
            playerManager.instance.currentAudio = "winGame";
            playerManager.instance.currentClip = 0;
            playerManager.instance.shouldStopClip = true;
            pointLight.color = new Color(0.035f, 0.789f, 0.347f);
            
            // DEN GÅR NÅ TIL GAME OVER MEN DEN SKAL TIL EN WINNE SCENE SOM IKKE ER LAGD!!
            playerManager.instance.Invoke("gameOverSceneChange", 26f);
        }
    }
}
