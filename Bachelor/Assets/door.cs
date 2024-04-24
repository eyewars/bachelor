using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour, interactable{
    private Transform[] doors = new Transform[2];
    
    public bool isOpen = false;
    private bool isUnlocked = false;
    public int keyId;
        
    public float doorSpeed;
    public float openRange;

    public LayerMask playerMask;
    public LayerMask wallMask;
    
    public void interact() {
        if (!isUnlocked) {
            if (playerManager.instance.keyIds.Contains(keyId)) {
                unlockDoor();
            }
        }
    }
    
    public void unlockDoor() {
        isUnlocked = true;
    }

    void Start() {
        // PASS PÅ NÅR DERE BUILDER AT DISSE INDEXENE BLIR RIKTIGE!!!!!
        doors[0] = transform.GetChild(0).GetChild(0);
        doors[1] = transform.GetChild(0).GetChild(1);

        StartCoroutine(doorTimer());

        if (keyId == 0) {
            isUnlocked = true;
        } else {
            isUnlocked = false;
        }
    }
    
    void Update() {
        if (isOpen) {
            if (doors[0].localPosition.x < 0.017f) {
                doors[0].localPosition = new Vector3(doors[0].localPosition.x + (0.001f * doorSpeed * Time.deltaTime), 0f, 0f);
                doors[1].localPosition = new Vector3(doors[1].localPosition.x - (0.001f * doorSpeed * Time.deltaTime), 0f, 0f);
            } else if (doors[0].localPosition.x < 0.017f) {
                doors[0].localPosition = new Vector3(0.017f, 0f, 0f);
                doors[1].localPosition = new Vector3(-0.017f, 0f, 0f);
            }
        } else {
            if (doors[0].localPosition.x > 0f) {
                doors[0].localPosition = new Vector3(doors[0].localPosition.x - (0.001f * doorSpeed * Time.deltaTime), 0f, 0f);
                doors[1].localPosition = new Vector3(doors[1].localPosition.x + (0.001f * doorSpeed * Time.deltaTime), 0f, 0f);
            } else if (doors[0].localPosition.x < 0f) {
                doors[0].localPosition = new Vector3(0f, 0f, 0f);
                doors[1].localPosition = new Vector3(0f, 0f, 0f);
            }
        }
    }

    private void checkPlayerProximity() {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, openRange, playerMask);

        if (rangeCheck.Length != 0) {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            Vector3 doorhandlePositionOffset = transform.position + directionToTarget;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            float distanceToTargetOffset = distanceToTarget - directionToTarget.magnitude;

            isOpen = !Physics.Raycast(doorhandlePositionOffset, directionToTarget, distanceToTargetOffset, wallMask);
        } else {
            isOpen = false;
        }
    }

    IEnumerator doorTimer() {
        // Bytt ut true etterhvert (for eksempel med så lenge spilleren har tapt eller whatever idk)
        while (true) {
            if (isUnlocked) {
                checkPlayerProximity();
            }
            
            yield return new WaitForSeconds(0.2f);
        }
    }
}