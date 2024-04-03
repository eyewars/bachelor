using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class aiNavigation : MonoBehaviour{
    public NavMeshAgent agent;
    private Vector3 walkPos;
    public Transform player;

    private bool isChasing = false;

    void Start() {
        //getNewWalkPosTwo();
        agent.SetDestination(player.position);
    }

    void Update() {
        if (!isChasing) {
           if (agent.remainingDistance < 0.1) {
               getNewWalkPos();
           }

           float distanceToPlayer = Vector3.Distance(player.position, transform.position);
           if (distanceToPlayer < 10) {
               isChasing = true;
           } 
        } else {
            agent.SetDestination(player.position);
            
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (distanceToPlayer < 10) {
                isChasing = false;
            } 
        }
    }
    void getNewWalkPos() {
        float x = Random.Range(-60f, 78f);
        float z = Random.Range(-60f, 78f);

        walkPos = new Vector3(x, 0, z);
        agent.SetDestination(walkPos);
    }
}