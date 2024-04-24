using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightswitchInteract : MonoBehaviour, interactable{
    public GameObject lampGroup;
    private List<Transform> lamps = new List<Transform>();
    private List<lamp> lampScript = new List<lamp>();

    private void Start() {
        for (int i = 0; i < lampGroup.transform.childCount; i++) { 
            lamps.Add(lampGroup.transform.GetChild(i));
        }

        for (int i = 0; i < lamps.Count; i++) {
            if (lamps[i].tag != "deadLamp") {
                lampScript.Add(lamps[i].GetComponent<lamp>());
            }
        }
    }

    public void interact() {
        for (int i = 0; i < lampScript.Count; i++) {
            lampScript[i].toogleLamp();
        }
    }
}