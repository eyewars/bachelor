using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour{
    public static playerManager instance;

    public bool hasFlashlight;
    public float maxBattery;
    public float battery;

    public List<int> keyIds = new List<int>();

    public bool roofToggle;

    public float standAudioMult;
    public float crouchAudioMult;
    public float normalAudioMult;
    public float runningAudioMult;

    public bool hasLost;

    void Awake() {
        if (instance != null) {
            Debug.LogError("BRO DET ER MER ENN EN PLAYERMANAGER!!!!!!!!!!!!!!!!!!");
            return;
        }

        instance = this;
    }

    void Start() {
        GameObject[] mapParts = GameObject.FindGameObjectsWithTag("roof");

        for (int i = 0; i < mapParts.Length; i++) {
            mapParts[i].SetActive(roofToggle);
        }
    }

    public void gameOverSceneChange() {
        Debug.Log("Nå bytter vi til end scenen");
    }
}