using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCam : MonoBehaviour{
    public float cameraSensitivity;
    public float cameraAcceleration;

    public Transform camera;

    public Transform hand;

    private float xPos;
    private float yPos;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        xPos += Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSensitivity;
        yPos += Input.GetAxis("Mouse X") * Time.deltaTime * cameraSensitivity;
        
        xPos = Mathf.Clamp(xPos, -90f, 90f);
        
        hand.localRotation = Quaternion.Euler(-xPos, yPos, 0);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, yPos, 0), cameraAcceleration * Time.deltaTime);
        camera.localRotation = Quaternion.Lerp(camera.localRotation, Quaternion.Euler(-xPos, 0, 0), cameraAcceleration * Time.deltaTime);

    }
}
