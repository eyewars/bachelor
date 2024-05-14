using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCam : MonoBehaviour{
    public float cameraSensitivity;
    public float cameraAcceleration;

    public Transform camera;

    public Transform hand;

    public Transform body;

    private float xPos;
    private float yPos;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if (!playerManager.instance.hasLost) {
            xPos += Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSensitivity;
            yPos += Input.GetAxis("Mouse X") * Time.deltaTime * cameraSensitivity;

            xPos = Mathf.Clamp(xPos, -90f, 90f);

            hand.localRotation = Quaternion.Euler(-xPos, yPos, 0f);

            //body.rotation = Quaternion.Lerp(body.rotation, Quaternion.Euler(0f, yPos, 0f), cameraAcceleration * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, yPos, 0f), cameraAcceleration * Time.deltaTime);
            camera.localRotation = Quaternion.Lerp(camera.localRotation, Quaternion.Euler(-xPos, 0f, 0f), cameraAcceleration * Time.deltaTime);

            //body.rotation = Quaternion.Euler(0f, yPos, 0f);
            //transform.localRotation = Quaternion.Euler(-xPos, 0f, 0f);
        }
    }
}