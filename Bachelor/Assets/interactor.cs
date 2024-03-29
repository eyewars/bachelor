using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface interactable{
    public void interact();
}

public class interactor : MonoBehaviour{
    public float interactRange;

    public Camera myCamera;

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            Ray r = new Ray(myCamera.transform.position, myCamera.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)) {
                if (hitInfo.collider.gameObject.TryGetComponent(out interactable interactObj)) {
                    interactObj.interact();
                }
            }
        }
    }
}