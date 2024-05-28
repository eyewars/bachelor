using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class lamp : MonoBehaviour{
    public bool isStrong;

    public bool blinking;

    private float strongIntesity = 100f;
    private float weakIntensity = 1f;

    public Material strongEmission;
    public Material weakEmission;
    public Material redEmission;

    private HDAdditionalLightData myLight;

    void Start() {
        myLight = transform.GetChild(1).GetComponent<HDAdditionalLightData>();

        changeIntensity();

        if (blinking) {
            StartCoroutine(blinkingTimer());
        }
    }

    public void toggleLamp() {
        if (isStrong) {
            isStrong = false;
        } else {
            isStrong = true;
        }

        changeIntensity();
    }

    private void changeIntensity() {
        if (isStrong) {
            myLight.intensity = strongIntesity;
            transform.GetChild(0).GetComponent<Renderer>().material = strongEmission;
        } else {
            myLight.intensity = weakIntensity;
            transform.GetChild(0).GetComponent<Renderer>().material = weakEmission;
        }
    }
    
    IEnumerator blinkingTimer() {
        while (true) {
            blink();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void blink() {
        float randomNum = Random.Range(0, 5f);

        if (randomNum > 4) {
            toggleLamp();
        }
    }
}