using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class door : MonoBehaviour, interactable{
    private Transform[] doors = new Transform[2];
    private Transform[] wheels = new Transform[2];

    public bool isOpen = false;
    private bool isUnlocked = false;
    public int keyId;

    public bool isBig;

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
        if (isBig) {
            doors[0] = transform.GetChild(0).GetChild(0);
            doors[1] = transform.GetChild(0).GetChild(1);
        } else {
            doors[0] = transform.GetChild(2).GetChild(0);
            doors[1] = transform.GetChild(2).GetChild(1);

            wheels[0] = doors[0].GetChild(0);
            wheels[1] = doors[1].GetChild(0);
        }


        StartCoroutine(doorTimer());

        if (keyId == 0) {
            isUnlocked = true;
        } else {
            isUnlocked = false;
        }
    }

    void Update() {
        if (isBig) {
            if (isOpen) {
                if (doors[0].localPosition.x < 0.017f) {
                    doors[0].localPosition = new Vector3(doors[0].localPosition.x + (0.001f * doorSpeed * Time.deltaTime), 0f, 0f);
                    doors[1].localPosition = new Vector3(doors[1].localPosition.x - (0.001f * doorSpeed * Time.deltaTime), 0f, 0f);
                } else if (doors[0].localPosition.x > 0.017f) {
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
        } else {
            if (isOpen) {
                // LOKKA: -0.2376854
                // ÅPEN: 1.471
                if (doors[1].localPosition.x < 1.471f) {
                    doors[1].localPosition = new Vector3(doors[1].localPosition.x + (0.2f * doorSpeed * Time.deltaTime), -0.08292082f, -0.7379031f);
                    } else if (doors[1].localPosition.x >= 1.471f) {
                        doors[1].localPosition = new Vector3(1.471f, -0.08292082f, -0.7379031f);

                        if (wheels[0].localEulerAngles.y < 170f) {
                            Vector3 currentAngles = wheels[0].localEulerAngles;
                            currentAngles.y += 50f * doorSpeed * Time.deltaTime;
                            wheels[0].localEulerAngles = currentAngles;
                            wheels[1].localEulerAngles = currentAngles;
                        } else if (wheels[0].localEulerAngles.y >= 170f) {
                            wheels[0].localEulerAngles = new Vector3(0f, 171f, 0f);
                            wheels[1].localEulerAngles = new Vector3(0f, 171f, 0f);

                            if (doors[0].localPosition.x < 1.471f) {
                                doors[0].localPosition = new Vector3(doors[0].localPosition.x + (0.2f * doorSpeed * Time.deltaTime), 0.1042567f, -0.7379031f);
                            } else if (doors[0].localPosition.x > 1.471f) {
                                doors[0].localPosition = new Vector3(1.471f, 0.1042567f, -0.7379031f);
                            }
                        }
                    }
                } else {
                if (doors[1].localPosition.x > -0.2376854f) {
                        doors[1].localPosition = new Vector3(doors[1].localPosition.x - (0.2f * doorSpeed * Time.deltaTime), -0.08292082f, -0.7379031f);
                    } else if (doors[1].localPosition.x <= -0.2376854f) {
                        doors[1].localPosition = new Vector3(-0.2376854f, -0.08292082f, -0.7379031f);

                        if (wheels[0].localEulerAngles.y > 10f) {
                            Vector3 currentAngles = wheels[0].localEulerAngles;
                            currentAngles.y -= 40f * doorSpeed * Time.deltaTime;
                            wheels[0].localEulerAngles = currentAngles;
                            wheels[1].localEulerAngles = currentAngles;
                        } else if (wheels[0].localEulerAngles.y <= 10f) {
                            wheels[0].localEulerAngles = new Vector3(0f, 9f, 0f);
                            wheels[1].localEulerAngles = new Vector3(0f, 9f, 0f);

                            if (doors[0].localPosition.x > -0.2376854f) {
                                doors[0].localPosition = new Vector3(doors[0].localPosition.x - (0.2f * doorSpeed * Time.deltaTime), 0.1042567f, -0.7379031f);
                            } else if (doors[0].localPosition.x < -0.2376854f) {
                                doors[0].localPosition = new Vector3(-0.2376854f, 0.1042567f, -0.7379031f);
                            }
                        }
                    }
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