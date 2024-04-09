using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCam : MonoBehaviour{
    public float cameraSensitivity;
    public float cameraAcceleration;

    public Transform orientation;
   

    float xRotation;
    float yRotation;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSensitivity;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(xRotation, yRotation, 0f), cameraAcceleration * 0.003f);
        
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        
    }
}
