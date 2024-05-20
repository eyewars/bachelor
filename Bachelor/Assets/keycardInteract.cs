using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycardInteract : MonoBehaviour, interactable{
    public int keyId;

    private AudioSource source;
    public  AudioClip[] interactSounds;

    private void Start() {
        source = GetComponent<AudioSource>();

        int randomNum = (int)Random.Range(0, interactSounds.Length - 1);
        source.clip = interactSounds[randomNum];
    }
    public void interact() {
        playerManager.instance.keyIds.Add(keyId);
        
        source.Play();
        
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("destroySelf", source.clip.length);
    }
    
    public void destroySelf() {
        Destroy(gameObject);
    }
}