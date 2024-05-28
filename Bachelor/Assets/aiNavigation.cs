using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class aiNavigation : MonoBehaviour{
    public int monsterId;
    public NavMeshAgent agent;
    private Vector3 walkPos;
    public GameObject player;
    private AudioSource playerSource;
    private playerMovement playerScript;

    public GameObject patrolPointHolder;
    private List<Transform> patrolPoints = new List<Transform>();
    private int currentPatrolIndex = 0;
    private int previousPatrolIndex = 0;

    public bool shouldLoop;
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

    public float walkSpeed;
    public float runSpeed;

    private AudioSource source;
    public AudioClip[] walkingSounds;
    public AudioClip[] chasingSounds;
    public AudioClip[] screamingSounds;

    public bool isTroll;

    void Start() {
        source = GetComponent<AudioSource>();

        agent.speed = walkSpeed;

        patrolPoints.AddRange(patrolPointHolder.GetComponentsInChildren<Transform>());
        patrolPoints.Remove(patrolPointHolder.transform);

        playerScript = player.GetComponent<playerMovement>();

        playerSource = player.GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        patrol();

        StartCoroutine(sensingTimer());

        if (isTroll){
            agent.speed = runSpeed;
        }
    }

    void Update() {
        checkForGameOver();
        if (!playerManager.instance.hasLost) {
            if (!isTroll){
                    if (!canSeePlayer) {
                    agent.speed = walkSpeed;

                    if ((agent.remainingDistance < 0.1) && !isIdling) {
                        previousPatrolIndex = currentPatrolIndex;
                        if (!shouldLoop) {
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
                        } else {
                            currentPatrolIndex++;

                            if (currentPatrolIndex == patrolPoints.Count) {
                                currentPatrolIndex = 0;
                            }
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
                    agent.speed = runSpeed;

                    agent.SetDestination(player.transform.position);

                    if (!canSeePlayer) {
                        patrol();
                    }
                }
            } else{
                if (!canSeePlayer){
                    agent.SetDestination(patrolPoints[0].position);
                } else {
                    agent.SetDestination(player.transform.position);
                }
            }
        }
    }

    void patrol() {
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);

        isIdling = false;
        animator.SetBool("isWalking", true);
    }

    IEnumerator sensingTimer() {
        while (true) {
            if (!playerManager.instance.hasLost) {
                findPlayerVisual(); 
                findPlayerAudio();
            }
            playSound();
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
                        playerManager.instance.changeProfile("chasing");
                        playerManager.instance.monsterSeenBy = monsterId;
                        CancelInvoke("outOfVision");
                        CancelInvoke("outOfAudio");

                        if (!playerSource.isPlaying){
                            playerSource.Play();
                        }
                    } else {
                        Invoke("outOfVision", 2.5f);
                    }
                }
            } else if (playerManager.instance.monsterSeenBy == monsterId){
                canSeePlayer = false;
                animator.SetBool("isChasing", false);
                playerManager.instance.changeProfile("normal");
                playerManager.instance.monsterSeenBy = 0;
                playerSource.Stop();
            }
        }
    }

    void outOfVision(){
        canSeePlayer = false;
        animator.SetBool("isChasing", false);
        playerManager.instance.changeProfile("normal");
        playerManager.instance.monsterSeenBy = 0;
        playerSource.Stop();
    }

    void findPlayerAudio() {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        float audioMultiplier;

        if (playerScript.movementType == 0) {
            audioMultiplier = playerManager.instance.crouchAudioMult;
        } else if (playerScript.movementType == 1) {
            audioMultiplier = playerManager.instance.normalAudioMult;
        } else if (playerScript.movementType == 2) {
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
                playerManager.instance.changeProfile("chasing");
                playerManager.instance.monsterSeenBy = monsterId;
                CancelInvoke("outOfAudio");
                CancelInvoke("outOfVision");
                if (!playerSource.isPlaying){
                    playerSource.Play();
                }
            } else if (playerManager.instance.monsterSeenBy == monsterId){
                Invoke("outOfAudio", 2.5f);
            }
        }
    }

    void outOfAudio(){
        canSeePlayer = false;
        animator.SetBool("isChasing", false);
        playerManager.instance.changeProfile("normal");
        playerManager.instance.monsterSeenBy = 0;
        playerSource.Stop();
    }

    void checkForGameOver() {
        if (!playerManager.instance.hasLost) {
            Collider[] rangeCheck = Physics.OverlapSphere(transform.position, 50, playerMask);

            if (rangeCheck.Length > 0) {
                Transform target = rangeCheck[0].transform;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (((distanceToTarget < 2f) && (canSeePlayer))) {
                    playerManager.instance.changeProfile("caught");
                    playerManager.instance.hasLost = true;
                    agent.SetDestination(transform.position);

                    playerManager.instance.Invoke("gameOverSceneChange", 2.5f);

                    player.GetComponent<playerMovement>().cameraBob.Play("None");

                    Transform head = player.transform.GetChild(0);
                    Transform camera = head.GetChild(0);

                    Vector3 directionToTarget = -(target.position - transform.position).normalized;

                    float dotProduct = Vector3.Dot(player.transform.forward.normalized, directionToTarget);
                    Vector3 crossProduct = Vector3.Cross(player.transform.forward.normalized, directionToTarget);

                    float angle = Mathf.Atan2(crossProduct.magnitude, dotProduct);
                    Vector3 axis = crossProduct.normalized;

                    Quaternion rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);

                    head.localRotation = rotation;

                    Vector3 eulerAngles = player.transform.eulerAngles;
                    eulerAngles.x = 0f;
                    eulerAngles.z = 0f;
                    player.transform.eulerAngles = eulerAngles;

                    Vector3 eulerAngles2 = head.localEulerAngles;
                    eulerAngles2.x = 0f;
                    eulerAngles2.z = 0f;
                    head.localEulerAngles = eulerAngles2;

                    Vector3 eulerAngles3 = camera.localEulerAngles;
                    eulerAngles3.x = 0f;
                    eulerAngles3.y = 0f;
                    eulerAngles3.z = 0f;
                    camera.localEulerAngles = eulerAngles3;

                    transform.LookAt(player.transform);

                    animator.SetBool("isCaught", true);
                }
            }
        }
    }

    void playSound() {
        if (!canSeePlayer) {
            if (!isTroll){
                if (!((walkingSounds.Contains(source.clip)) && (source.isPlaying))) {
                    int randomNum = (int)Random.Range(0, walkingSounds.Length - 1);
                    source.clip = walkingSounds[randomNum]; 
                    source.Play();
                } 
            }  
        } else if (playerManager.instance.hasLost) {
            if (!((screamingSounds.Contains(source.clip)) && (source.isPlaying))) {
                int randomNum = (int)Random.Range(0, screamingSounds.Length - 1);
                source.clip = screamingSounds[randomNum]; 
                source.Play();
            }
        } else {
            if (!((chasingSounds.Contains(source.clip)) && (source.isPlaying))) {
                int randomNum = (int)Random.Range(0, chasingSounds.Length - 1);
                source.clip = chasingSounds[randomNum]; 
                source.Play();
            }
        }
    }
}