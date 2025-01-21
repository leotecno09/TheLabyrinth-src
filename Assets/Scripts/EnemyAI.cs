using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour // booooooooooooohhhh da rivederrrr
{
    public Transform player;
    public AudioSource audioSource;
    public AudioClip music1;
    public AudioClip music2;
    public NavMeshAgent enemy;

    public float visionAngle = 45f;
    public float visionDistance = 10f;
    public LayerMask obstructionMask;
    private bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInSight()) {
            isChasing = true;
            enemy.SetDestination(player.position);

            Debug.Log("Player has been seen, CHASING IT!!");
        } else {
            if (isChasing) {
                isChasing = false;
            }

            if (!isChasing) {
                RandomMovement();
            }
        }
    }

    private bool PlayerInSight() {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > visionDistance) {
            return false;
        }

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer > visionAngle / 2f) {
            return false;
        }

        if (Physics.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstructionMask)) {
            return false;
        }

        return true;
    }

    private void RandomMovement() {
        if (!enemy.hasPath || enemy.remainingDistance < 1f) {
            Vector3 randomDirection = Random.insideUnitSphere * 10f;
            randomDirection += transform.position;
            
            UnityEngine.AI.NavMeshHit navHit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, 10f, UnityEngine.AI.NavMesh.AllAreas)) {
                enemy.SetDestination(navHit.position);
            }
        }
    }
}
