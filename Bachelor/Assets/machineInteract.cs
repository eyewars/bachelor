using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineInteract : MonoBehaviour, IInteractableStart{
    public int machineId;
    
    private AudioSource source;
    public AudioClip[] interactSounds;

    public string HoverText => "Interact with machine";

    void Start() {
        source = GetComponent<AudioSource>();
        
        int randomNum = (int)Random.Range(0, interactSounds.Length - 1);
        source.clip = interactSounds[randomNum];
    }
    
    public void InteractStart() {
        playerManager.instance.machineIds.Add(machineId);
        
        source.Play();
        
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        Invoke("destroySelf", source.clip.length);
    }
    
    public void destroySelf() {
        Destroy(gameObject);
    }
}