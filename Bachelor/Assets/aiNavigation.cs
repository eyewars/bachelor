using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class aiNavigation : MonoBehaviour{
    public NavMeshAgent agent;
    private Vector3 walkPos;
    public GameObject player;
    private playerMovement playerScript;

    public GameObject patrolPointHolder;
    private List<Transform> patrolPoints = new List<Transform>();
    private int currentPatrolIndex = 0;
    private int previousPatrolIndex = 0;

    private bool patrolForward = true;

    private bool isIdling = false;

    private bool canSeePlayer;

    private bool foundPlayerWithSight;

    public float visualRange;
    public float visualAngle;

    public float audioThreshold;

    public LayerMask playerMask;
    public LayerMask wallMask;

    private Animator animator;

    void Start() {
        patrolPoints.AddRange(patrolPointHolder.GetComponentsInChildren<Transform>());
        patrolPoints.Remove(patrolPointHolder.transform);

        playerScript = player.GetComponent<playerMovement>();

        animator = GetComponent<Animator>();

        patrol();

        StartCoroutine(sensingTimer());
    }

    void Update() {
        if (!canSeePlayer) {
            if ((agent.remainingDistance < 0.1) && !isIdling) {
                previousPatrolIndex = currentPatrolIndex;
                if (currentPatrolIndex == 0) {
                    patrolForward = true;
                } else if (currentPatrolIndex == (patrolPoints.Count - 1)) {
                    patrolForward = false;
                }

                if (patrolForward) {
                    currentPatrolIndex++;
                } else {
                    currentPatrolIndex--;
                }

                if (patrolPoints[previousPatrolIndex].gameObject.tag == "shouldIdle") {
                    isIdling = true;
                    animator.SetBool("isWalking", false);
                    Invoke(nameof(patrol), 2.5f);
                } else {
                    patrol();
                }
            }
        } else {
            agent.SetDestination(player.transform.position);

            if (!canSeePlayer) {
                patrol();
            }
        }
    }

    void patrol() {
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);

        isIdling = false;
        animator.SetBool("isWalking", true);
    }

    IEnumerator sensingTimer() {
        // Bytt ut true etterhvert (for eksempel med så lenge spilleren har tapt eller whatever idk)
        while (true) {
            findPlayerVisual();
            findPlayerAudio();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void findPlayerVisual() {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, visualRange, playerMask);

        if ((canSeePlayer == false) || ((canSeePlayer == true) && (foundPlayerWithSight == true))) {
            if (rangeCheck.Length != 0) {
                Transform target = rangeCheck[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < (visualAngle / 2)) {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    Vector3 adjustedPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                    
                    if (!Physics.Raycast(adjustedPosition, directionToTarget, distanceToTarget, wallMask)) {
                        canSeePlayer = true;
                        animator.SetBool("isChasing", true);
                        foundPlayerWithSight = true;
                    } else {
                        canSeePlayer = false;
                        animator.SetBool("isChasing", false);
                    }
                }
            } else {
                canSeePlayer = false;
                animator.SetBool("isChasing", false);
            }
        }
    }

    void findPlayerAudio() {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        float audioMultiplier;

        if (playerScript.movementType == 0) {
            audioMultiplier = playerManager.instance.crouchAudioMult;
        } else if (playerScript.movementType == 1) {
            audioMultiplier = playerManager.instance.normalAudioMult;
        } else if (playerScript.movementType == 2){
            audioMultiplier = playerManager.instance.runningAudioMult;
        } else {
            audioMultiplier = playerManager.instance.standAudioMult;
        }

        float audioValue = (100 * audioMultiplier) / Mathf.Pow(distanceToPlayer, 2);

        if ((canSeePlayer == false) || ((canSeePlayer == true) && (foundPlayerWithSight == false))) {
            if (audioValue > audioThreshold) {
                canSeePlayer = true;
                animator.SetBool("isChasing", true);
                foundPlayerWithSight = false;
            } else {
                canSeePlayer = false;
                animator.SetBool("isChasing", false);
            }
        }
    }
}