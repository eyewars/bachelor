using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class setVolume : MonoBehaviour{
    public AudioMixer mixer;
    public Slider mySlider;

    public static float sliderAudiovalue = 1f;

    public void setLevel(float sliderValue){
        mixer.SetFloat("audioVolume", Mathf.Log10(sliderValue) * 20);

        sliderAudiovalue = sliderValue;
    }

    void Awake(){
        mySlider.value = sliderAudiovalue;
    }
}
