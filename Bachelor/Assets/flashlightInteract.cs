using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class flashlightInteract : MonoBehaviour, interactable{
    public TextMeshProUGUI batteryText;
    public GameObject flashLightModel;
    
    private AudioSource source;
    public  AudioClip[] interactSounds;

    private void Start() {
        source = GetComponent<AudioSource>();

        int randomNum = (int)Random.Range(0, interactSounds.Length - 1);
        source.clip = interactSounds[randomNum];
    }
    
    public void interact() {
        playerManager.instance.hasFlashlight = true;
        batteryText.enabled = true;
        flashLightModel.SetActive(true);
        
        source.Play();
        
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        Invoke("destroySelf", source.clip.length);
    }
    
    public void destroySelf() {
        Destroy(gameObject);
    }
}