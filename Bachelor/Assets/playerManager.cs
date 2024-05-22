using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class playerManager : MonoBehaviour{
    public static playerManager instance;

    public bool hasFlashlight;
    public float maxBattery;
    public float battery;

    public List<int> keyIds = new List<int>();
    public List<int> machineIds = new List<int>();

    public bool roofToggle;

    public float standAudioMult;
    public float crouchAudioMult;
    public float normalAudioMult;
    public float runningAudioMult;

    public bool hasLost;

    public bool isTyping;

    public Volume myVolume;
    public VolumeProfile normalProfile;
    public VolumeProfile chasingProfile;
    public VolumeProfile caughtProfile;

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
        
        changeProfile("normal");
    }

    public void gameOverSceneChange() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(2);
    }

    public void changeProfile(string profile) {
        if (profile == "normal") {
            myVolume.profile = normalProfile;
        } else if (profile == "chasing") {
            myVolume.profile = chasingProfile;
        } else if (profile == "caught") {
            myVolume.profile = caughtProfile;
        }
    }
}