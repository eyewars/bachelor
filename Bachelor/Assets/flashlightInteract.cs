using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashlightInteract : MonoBehaviour, IInteractableStart{
    public Image battery;
    public GameObject flashLightModel;
    
    private AudioSource source;
    public  AudioClip[] interactSounds;

    public string HoverText => "Pick up flashlight";

    private void Start() {
        source = GetComponent<AudioSource>();

        int randomNum = (int)Random.Range(0, interactSounds.Length - 1);
        source.clip = interactSounds[randomNum];
    }
    
    public void InteractStart() {
        playerManager.instance.hasFlashlight = true;
        battery.enabled = true;
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