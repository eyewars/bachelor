using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface interactable{
    public void interact();
}

public class interactor : MonoBehaviour{
    public float interactRange;

    public Camera myCamera;

	private terminal myTerminal;

    void Update() {
		if (Input.GetKeyDown(KeyCode.F)) {
            Ray r = new Ray(myCamera.transform.position, myCamera.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)) {
                if (hitInfo.collider.gameObject.TryGetComponent(out interactable interactObj)) {
                    interactObj.interact();
                }
            }
        }

		if (Input.GetKey(KeyCode.F)){
			Ray r = new Ray(myCamera.transform.position, myCamera.transform.forward);
			if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange)){
				if (hitInfo.collider.gameObject.TryGetComponent(out interactable interactObj)){
					if (hitInfo.collider.gameObject.tag == "terminal") {
						myTerminal = hitInfo.collider.gameObject.GetComponent<terminal>();

                        if ((!playerManager.instance.isTyping) && (!myTerminal.isShowingMap)){
                            playerManager.instance.isTyping = true;
							myTerminal.startAnimation();
							myTerminal.showMap();
                        }
                    }
				}
			}else {
				playerManager.instance.isTyping = false;
				if (myTerminal != null){
					myTerminal.stopAnimation();
					myTerminal.cancelMap();
				}
        	}
		}

		if (Input.GetKeyUp(KeyCode.F)){
            playerManager.instance.isTyping = false;
			if (myTerminal != null){
				myTerminal.stopAnimation();
				myTerminal.cancelMap();
			}
        }
    }
}