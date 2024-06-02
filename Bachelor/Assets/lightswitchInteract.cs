using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightswitchInteract : MonoBehaviour, IInteractableStart{
    public GameObject lampGroup;
    private List<Transform> lamps = new List<Transform>();
    private List<lamp> lampScript = new List<lamp>();
    
    private AudioSource source;
    public  AudioClip[] interactSounds;

    private Transform mySwitch;

    public string HoverText => "Toggle lights";

    private void Start() {
        source = GetComponent<AudioSource>();
        
        for (int i = 0; i < lampGroup.transform.childCount; i++) { 
            lamps.Add(lampGroup.transform.GetChild(i));
        }

        for (int i = 0; i < lamps.Count; i++) {
            if (lamps[i].tag != "deadLamp") {
                lampScript.Add(lamps[i].GetComponent<lamp>());
            }
        }

        mySwitch = transform.GetChild(2);

        if (lampScript[0].isStrong) {
            mySwitch.localEulerAngles = new Vector3(5f, 0, 0);
        } else {
            mySwitch.localEulerAngles = new Vector3(-5f, 0, 0);
        }
    }

    public void InteractStart() {
        for (int i = 0; i < lampScript.Count; i++) {
            if (!lampScript[i].blinking) {
                lampScript[i].toggleLamp();
            }
        }

        if (lampScript[0].isStrong) {
            source.clip = interactSounds[1];
            mySwitch.localEulerAngles = new Vector3(5f, 0, 0);
        } else {
            source.clip = interactSounds[0];
            mySwitch.localEulerAngles = new Vector3(-5f, 0, 0);
        }
        
        source.Play();
    }
}