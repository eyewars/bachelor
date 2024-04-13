using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class aiNavigation : MonoBehaviour{
    public NavMeshAgent agent;
    private Vector3 walkPos;
    public Transform player;

    public List<GameObject> patrolPoints = new List<GameObject>();
    private int currentPatrolIndex = 0;
    private int previousPatrolIndex = 0;

    private bool patrolForward = true;

    private bool isIdling = false;

    private bool canSeePlayer;

    public float visualRange;
    public float visualAngle;

    public LayerMask playerMask;
    public LayerMask wallMask;

    void Start() {
        patrol();

        StartCoroutine(visualTimer());
    }

    void Update() {
        if (!canSeePlayer) {
            if ((agent.remainingDistance < 0.1) && !isIdling) {
                previousPatrolIndex = currentPatrolIndex;
                if (currentPatrolIndex == 0){
                    patrolForward = true;
                } else if (currentPatrolIndex == (patrolPoints.Count - 1)){
                    patrolForward = false;
                }

                if (patrolForward){
                    currentPatrolIndex++;
                }else{
                    currentPatrolIndex--;
                }

                if(patrolPoints[previousPatrolIndex].tag == "shouldIdle"){
                    isIdling = true;
                    Invoke(nameof(patrol), 2.5f);
                }
                else {
                    patrol();
                }
            }
        } else {
            agent.SetDestination(player.position);
            
            if (!canSeePlayer) {
                patrol();
            } 
        }
    }

    void patrol(){
        agent.SetDestination(patrolPoints[currentPatrolIndex].transform.position);
        
        isIdling = false;
    }

    IEnumerator visualTimer(){
        // Bytt ut true etterhvert (for eksempel med så lenge spilleren har tapt eller whatever idk)
        while (true){
            findPlayerVisual();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void findPlayerVisual(){
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, visualRange, playerMask);

        if (rangeCheck.Length != 0){
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            
            if (Vector3.Angle(transform.forward, directionToTarget) < (visualAngle / 2)){
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, wallMask)){
                    canSeePlayer = true;
                }
                else {
                    canSeePlayer = false;
                }
            }
        } else{
            canSeePlayer = false;
        }
    }
}