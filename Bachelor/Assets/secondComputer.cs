using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class secondComputer : MonoBehaviour{
    public VideoPlayer leftPlayer;
    public VideoPlayer middlePlayer;
    public VideoPlayer rightPlayer;

    public VideoClip matrix1;
    public VideoClip matrix3;

    public VideoClip waveform;

    public VideoClip[] machineProgress;

    private bool hasPlayedWinGameWaveform;
    private bool hasStartedWinGameWaveform;

    private bool hasChangedToMatrixMiddle;

    private int machineProgressIsPlaying;

    void Start(){
        hasPlayedWinGameWaveform = false;
        hasStartedWinGameWaveform = false;
        hasChangedToMatrixMiddle = false;
        machineProgressIsPlaying = 0;

        rightPlayer.clip = matrix3;
        rightPlayer.Play();
        
        middlePlayer.clip = matrix1;
        middlePlayer.playbackSpeed = 2.4f;
        middlePlayer.Play();

        leftPlayer.clip = machineProgress[0];
        leftPlayer.Play();
    }

    void Update(){
        if (playerManager.instance.currentAudio == "winGame"){
            if (!hasPlayedWinGameWaveform){
                middlePlayer.Stop();
                middlePlayer.isLooping = false;
                middlePlayer.clip = waveform;
                middlePlayer.playbackSpeed = 1f;
                middlePlayer.Play();
                hasPlayedWinGameWaveform = true;
                hasChangedToMatrixMiddle = false;
            } else if (!hasChangedToMatrixMiddle && !middlePlayer.isPlaying && hasStartedWinGameWaveform){
                middlePlayer.Stop();
                middlePlayer.isLooping = true;
                middlePlayer.clip = matrix1;
                middlePlayer.playbackSpeed = 2.4f;
                middlePlayer.Play();
                hasChangedToMatrixMiddle = true;
            } 

            if (!hasStartedWinGameWaveform && middlePlayer.isPlaying){
                hasStartedWinGameWaveform = true;
            }
        }

        if (playerManager.instance.machineIdsUsed.Count == 1){
            if (machineProgressIsPlaying != 1){
                leftPlayer.Stop();
                leftPlayer.clip = machineProgress[1];
                leftPlayer.Play();
                machineProgressIsPlaying = 1;
            }
        }

        if (playerManager.instance.machineIdsUsed.Count == 2){
            if (machineProgressIsPlaying != 2){
                leftPlayer.Stop();
                leftPlayer.clip = machineProgress[2];
                leftPlayer.Play();
                machineProgressIsPlaying = 2;
            }
        }

        if (playerManager.instance.machineIdsUsed.Count == 3){
            if (machineProgressIsPlaying != 3){
                leftPlayer.Stop();
                leftPlayer.clip = machineProgress[3];
                leftPlayer.Play();
                machineProgressIsPlaying = 3;
            }
        }

        if (playerManager.instance.machineIdsUsed.Count == 4){
            if (machineProgressIsPlaying != 4){
                leftPlayer.Stop();
                leftPlayer.clip = machineProgress[4];
                leftPlayer.Play();
                machineProgressIsPlaying = 4;
            }
        }

        if (playerManager.instance.machineIdsUsed.Count == 5){
            if (machineProgressIsPlaying != 5){
                leftPlayer.Stop();
                leftPlayer.clip = machineProgress[5];
                leftPlayer.Play();
                machineProgressIsPlaying = 5;
            }
        }
    }
}
