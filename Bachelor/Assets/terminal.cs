using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class terminal : MonoBehaviour, IInteractableHold, IInteractableEnd{
    private AudioSource source;
    public AudioClip interactSound;
    public AudioClip interactFinishSound;

    private VideoPlayer player;
    public VideoClip matrix;
    public VideoClip startClip;
    public VideoClip mapClip;

    public bool isShowingMap;

    public string HoverText => isShowingMap ? "" : playerManager.instance.isTyping ? "Turning on..." : "Turn on terminal (hold)";
    
    public void InteractEnd()
    {
        playerManager.instance.isTyping = false;
        stopAnimation();
        cancelMap();
    }

    void Start() {
        source = GetComponent<AudioSource>();

        player = transform.GetChild(0).gameObject.GetComponent<VideoPlayer>();

        player.clip = startClip;
    }
    
    void Update() {
        
    }

    public void showMap(){
        Invoke("showMap2", 3.72f);
    }

    public void cancelMap(){
        CancelInvoke("showMap2");
    }

    private void showMap2(){
        if (playerManager.instance.isTyping){
            isShowingMap = true;
            source.clip = interactFinishSound;
            source.Play();

            player.clip = mapClip;
            player.Play();
        }
    }

    public void startAnimation(){
        if (!isShowingMap){
            source.clip = interactSound;
            source.Play();

            player.clip = matrix;
            player.Play();
        }
        
    }

    public void stopAnimation(){
        if (!isShowingMap){
            source.Stop();

            player.clip = startClip;
            player.Play();
        }
    }
    
    public void InteractHold()
    {
        if ((!playerManager.instance.isTyping) && (!isShowingMap))
        {
            playerManager.instance.isTyping = true;
            startAnimation();
            showMap();
        }
    }
}