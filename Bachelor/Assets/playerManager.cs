using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour{
    public static playerManager instance;

    public bool hasFlashlight;
    public float maxBattery;
    public float battery;
    
    void Awake(){
        if (instance != null){
            Debug.LogError("BRO DET ER MER ENN EN PLAYERMANAGER!!!!!!!!!!!!!!!!!!");
            return;
        }
        instance = this;
    }
    void Start() { }
    
    void Update() { }
}