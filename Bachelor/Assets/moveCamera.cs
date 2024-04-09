using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour{
    public Transform camera;

    void Update(){
        camera.position = transform.position;
    }
}
