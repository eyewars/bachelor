using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class introVoice : MonoBehaviour{
    public AudioSource source;
    public AudioSource alarmSource;

    public TextMeshProUGUI subtitles;

    public AudioClip pp;

    public AudioClip[] start;
    public string[] startSub;

    public AudioClip[] hallDoorOpen;
    public string[] hallDoorOpenSub;

    public AudioClip[] roomDoorOpen;
    public string[] roomDoorOpenSub;

    public AudioClip[] shipDoorOpen;
    public string[] shipDoorOpenSub;

    public AudioClip[] winGame;
    public string[] winGameSub;

    private float colorChangeTimer;
    private float colorChangeTimerMax;
    private int colorChangeTimerAmount;


    void Start(){
        source.clip = pp;

        source.Play();
        playerManager.instance.currentAudio = "pp";
        playerManager.instance.currentClip = 0;

        alarmSource.Play();

        subtitles.enabled = false;

        if (playerManager.instance.alarmProfile.TryGet(out ColorAdjustments colorAdjust)){
            colorAdjust.postExposure.value = 0f;
        }

        colorChangeTimerMax = 0.85f;
        colorChangeTimerAmount = 0;
    }

    void Update(){
        if (playerManager.instance.shouldStopClip){
            source.Stop();
            alarmSource.Stop();
            playerManager.instance.changeProfile("normal");
            playerManager.instance.shouldStopClip = false;
        }

        if (playerManager.instance.currentAudio == "pp"){
            if (!source.isPlaying){
                playerManager.instance.currentAudio = "start";
                alarmSource.Stop();
                playerManager.instance.changeProfile("normal");
            } else{
                colorChangeTimer += 1 * Time.deltaTime;

                if (colorChangeTimer >= colorChangeTimerMax){
                    colorChangeTimer -= colorChangeTimerMax;

                    if (playerManager.instance.alarmProfile.TryGet(out ColorAdjustments colorAdjust)){
                        if (colorChangeTimerAmount == 8){
                            colorAdjust.postExposure.value = -20f;
                        } else if (colorChangeTimerAmount < 8){
                            if (colorAdjust.postExposure.value == 0){
                                colorAdjust.postExposure.value = -20f;
                                colorChangeTimerMax = 0.9f;
                            } else {
                                colorAdjust.postExposure.value = 0f;
                                colorChangeTimerMax = 1.1f;
                            }
                            colorChangeTimerAmount++; 
                        }
                    } 
                }   
            }
        }

        if (playerManager.instance.currentAudio == "start"){
            if ((!source.isPlaying) && (playerManager.instance.currentClip <= (start.Length - 1))){
                subtitles.enabled = true;
                source.clip = start[playerManager.instance.currentClip];
                subtitles.text = startSub[playerManager.instance.currentClip];
                source.Play();
                playerManager.instance.currentClip++;
            } else if((!source.isPlaying) && (playerManager.instance.currentClip == start.Length)){
                subtitles.enabled = false;
            }
        }

        if (playerManager.instance.currentAudio == "hallDoorOpen"){
            if ((!source.isPlaying) && (playerManager.instance.currentClip <= (hallDoorOpen.Length - 1))){
                subtitles.enabled = true;
                source.clip = hallDoorOpen[playerManager.instance.currentClip];
                subtitles.text = hallDoorOpenSub[playerManager.instance.currentClip];
                source.Play();
                playerManager.instance.currentClip++;
            } else if((!source.isPlaying) && (playerManager.instance.currentClip == hallDoorOpen.Length)){
                subtitles.enabled = false;
            }
        }

        if (playerManager.instance.currentAudio == "roomDoorOpen"){
            if ((!source.isPlaying) && (playerManager.instance.currentClip <= (roomDoorOpen.Length - 1))){
                subtitles.enabled = true;
                source.clip = roomDoorOpen[playerManager.instance.currentClip];
                subtitles.text = roomDoorOpenSub[playerManager.instance.currentClip];
                source.Play();
                playerManager.instance.currentClip++;
            } else if((!source.isPlaying) && (playerManager.instance.currentClip == roomDoorOpen.Length)){
                subtitles.enabled = false;
            }
        }

        if (playerManager.instance.currentAudio == "shipDoorOpen"){
            if ((!source.isPlaying) && (playerManager.instance.currentClip <= (shipDoorOpen.Length - 1))){
                subtitles.enabled = true;
                source.clip = shipDoorOpen[playerManager.instance.currentClip];
                subtitles.text = shipDoorOpenSub[playerManager.instance.currentClip];
                source.Play();
                playerManager.instance.currentClip++;
            } else if((!source.isPlaying) && (playerManager.instance.currentClip == shipDoorOpen.Length)){
                subtitles.enabled = false;
            }
        }

        if (playerManager.instance.currentAudio == "winGame"){
            if ((!source.isPlaying) && (playerManager.instance.currentClip <= (winGame.Length - 1))){
                subtitles.enabled = true;
                source.clip = winGame[playerManager.instance.currentClip];
                subtitles.text = winGameSub[playerManager.instance.currentClip];
                source.Play();
                playerManager.instance.currentClip++;
            } else if((!source.isPlaying) && (playerManager.instance.currentClip == winGame.Length)){
                subtitles.enabled = false;
            }
        }
    }
}