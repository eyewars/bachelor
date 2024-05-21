using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface interactable{
    public void interact();
}

public class interactor : MonoBehaviour{
    public float interactRange;

    public Camera myCamera;

    private bool isTyping;

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            Ray r = new Ray(myCamera.transform.position, myCamera.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)) {
                if (hitInfo.collider.gameObject.TryGetComponent(out interactable interactObj)) {
                    interactObj.interact();

                    if (hitInfo.collider.gameObject.tag == "terminal") {
                        isTyping = true;
                        Debug.Log(isTyping);
                    }
                }
            } else {
                isTyping = false;
            }
        } else if (Input.GetKeyUp(KeyCode.F)) {
            isTyping = false;
        }
    }
}