using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class firstComputer : MonoBehaviour{
    public VideoPlayer leftPlayer;
    public VideoPlayer middlePlayer;
    public VideoPlayer rightPlayer;

    public VideoClip matrix1;
    public VideoClip matrix2;
    public VideoClip matrix3;
    public VideoClip errorClip;

    public VideoClip[] waveforms;

    private bool hasChangedToMatrixSides;
    private bool hasChangedToMatrixMiddle;

    private bool hasPlayedStartWaveform;
    private bool hasPlayedHallDoorWaveform;
    private bool hasPlayedRoomDoorWaveform;
    private bool hasPlayedShipDoorWaveform;

    private bool hasStartedStartWaveform;
    private bool hasStartedHallDoorWaveform;
    private bool hasStartedRoomDoorWaveform;
    private bool hasStartedShipDoorWaveform;

    void Start(){
        hasChangedToMatrixSides = false;
        hasChangedToMatrixMiddle = false;

        hasPlayedStartWaveform = false;
        hasPlayedHallDoorWaveform = false;
        hasPlayedRoomDoorWaveform = false;
        hasPlayedShipDoorWaveform = false;

        hasStartedStartWaveform = false;
        hasStartedHallDoorWaveform = false;
        hasStartedRoomDoorWaveform = false;
        hasStartedShipDoorWaveform = false;

        leftPlayer.clip = errorClip;
        middlePlayer.clip = errorClip;
        rightPlayer.clip = errorClip;

        leftPlayer.Play();
        middlePlayer.Play();
        rightPlayer.Play();
    }

    void Update(){
        if (!hasChangedToMatrixSides){
            if (playerManager.instance.currentAudio != "pp"){
                hasChangedToMatrixSides = true;
                leftPlayer.Stop();
                rightPlayer.Stop();

                leftPlayer.clip = matrix1;
                rightPlayer.clip = matrix2;

                leftPlayer.Play();
                rightPlayer.Play();
            }
        }
        

        if (playerManager.instance.currentAudio == "start"){
            if (!hasPlayedStartWaveform){
                Debug.Log("Nå er vi på den første plassen");
                middlePlayer.Stop();
                middlePlayer.isLooping = false;
                middlePlayer.clip = waveforms[0];
                middlePlayer.playbackSpeed = 1f;
                middlePlayer.Play();
                hasPlayedStartWaveform = true;
                hasChangedToMatrixMiddle = false;
            } else if (!hasChangedToMatrixMiddle && !middlePlayer.isPlaying && hasStartedStartWaveform){
                Debug.Log("Nå er han ferdig med å yappe");
                middlePlayer.Stop();
                middlePlayer.isLooping = true;
                middlePlayer.clip = matrix3;
                middlePlayer.playbackSpeed = 0.7f;
                middlePlayer.Play();
                hasChangedToMatrixMiddle = true;
            }

            if (!hasStartedStartWaveform && middlePlayer.isPlaying){
                hasStartedStartWaveform = true;
            }
        }

        if (playerManager.instance.currentAudio == "hallDoorOpen"){
            if (!hasPlayedHallDoorWaveform){
                middlePlayer.Stop();
                middlePlayer.isLooping = false;
                middlePlayer.clip = waveforms[1];
                middlePlayer.playbackSpeed = 1f;
                middlePlayer.Play();
                hasPlayedHallDoorWaveform = true;
                hasChangedToMatrixMiddle = false;
            } else if (!hasChangedToMatrixMiddle && !middlePlayer.isPlaying && hasStartedHallDoorWaveform){
                middlePlayer.Stop();
                middlePlayer.isLooping = true;
                middlePlayer.clip = matrix3;
                middlePlayer.playbackSpeed = 0.7f;
                middlePlayer.Play();
                hasChangedToMatrixMiddle = true;
            }

            if (!hasStartedHallDoorWaveform && middlePlayer.isPlaying){
                hasStartedHallDoorWaveform = true;
            }
        }

        if (playerManager.instance.currentAudio == "roomDoorOpen"){
            if (!hasPlayedRoomDoorWaveform){
                middlePlayer.Stop();
                middlePlayer.isLooping = false;
                middlePlayer.clip = waveforms[2];
                middlePlayer.playbackSpeed = 1f;
                middlePlayer.Play();
                hasPlayedRoomDoorWaveform = true;
                hasChangedToMatrixMiddle = false;
            } else if (!hasChangedToMatrixMiddle && !middlePlayer.isPlaying && hasStartedRoomDoorWaveform){
                middlePlayer.Stop();
                middlePlayer.isLooping = true;
                middlePlayer.clip = matrix3;
                middlePlayer.playbackSpeed = 0.7f;
                middlePlayer.Play();
                hasChangedToMatrixMiddle = true;
            } 

            if (!hasStartedRoomDoorWaveform && middlePlayer.isPlaying){
                hasStartedRoomDoorWaveform = true;
            }
        }

        if (playerManager.instance.currentAudio == "shipDoorOpen"){
            if (!hasPlayedShipDoorWaveform){
                middlePlayer.Stop();
                middlePlayer.isLooping = false;
                middlePlayer.clip = waveforms[3];
                middlePlayer.playbackSpeed = 1f;
                middlePlayer.Play();
                hasPlayedShipDoorWaveform = true;
                hasChangedToMatrixMiddle = false;
            } else if (!hasChangedToMatrixMiddle && !middlePlayer.isPlaying && hasStartedShipDoorWaveform){
                middlePlayer.Stop();
                middlePlayer.isLooping = true;
                middlePlayer.clip = matrix3;
                middlePlayer.playbackSpeed = 0.7f;
                middlePlayer.Play();
                hasChangedToMatrixMiddle = true;
            } 

            if (!hasStartedShipDoorWaveform && middlePlayer.isPlaying){
                hasStartedShipDoorWaveform = true;
            }
        }
    }
}
