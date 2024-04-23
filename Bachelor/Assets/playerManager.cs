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

    void Awake(){
        if (instance != null){
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
    
    void Update() { }
}