using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycardInteract : MonoBehaviour, IInteractableStart{
    public int keyId;

    private AudioSource source;
    public  AudioClip[] interactSounds;

    public string HoverText => "Pick up key-card";

    private void Start() {
        source = GetComponent<AudioSource>();

        int randomNum = (int)Random.Range(0, interactSounds.Length - 1);
        source.clip = interactSounds[randomNum];
    }
    public void InteractStart() {
        playerManager.instance.keyIds.Add(keyId);
        
        source.Play();
        
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        for (int i = 0; i < 4; i++){
            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
        Invoke("destroySelf", source.clip.length);
    }
    
    public void destroySelf() {
        Destroy(gameObject);
    }
}