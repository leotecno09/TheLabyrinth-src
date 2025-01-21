using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource;
    public AudioClip music1;
    public AudioClip music2;
    //public AudioClip jumpscare;
    //public Transform playerCamera;
    //public Transform enemyCamera;
    public Logic logic;
    public Jumpscare jumpscareScript;
    //public Transform player;   
    private NavMeshAgent agent;
    //public AnimationClip scream;
    private Animator animator;
    private float warnDistance = 90f; // 60 era quello "decente"
    private float jumpscareDistance = 25f;
    //private float transitionSpeed = 1.0f;
    //private float transitionProgress = 0.0f;
    private bool sawPlayer = false;
    private bool hasBeenJumpscared = false;
    public float walkRadius = 100f;
    // Start is called before the first frame update
    void Start()
    {
        //enemyCamera.gameObject.SetActive(false);

        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        audioSource.clip = music1;
        audioSource.Play();

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas)) {
            transform.position = hit.position;
        } else {
            Debug.Log("Enemy NON è su una superficie navigabile");
        }

        WalkRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (player != null) {
            agent.SetDestination(player.position);
        }*/

        // Check se ha raggiunto posizione randommm
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !sawPlayer) {
            WalkRandomly();
        }

        // Giocatore entrato nel raggio di visione
        if (Vector3.Distance(transform.position, player.position) <= warnDistance && !hasBeenJumpscared) {
            if (audioSource.clip != music2) {
                audioSource.Stop();
                audioSource.clip = music2;
                audioSource.Play();
            }

            agent.SetDestination(player.position);
                
            //animator.speed = 1.5f;
            animator.Play("Running1"); // Running1 se si vuole mettere animazione corsa, Crawl se si vuole che cammini rasoterra
            agent.speed = 48f;
            //agent.stoppingDistance = 0;

            sawPlayer = true;

            Debug.Log("FOLLOWING THE DAMN PLAYERRRR!!!");

            if (Vector3.Distance(transform.position, player.position) <= jumpscareDistance && sawPlayer && !hasBeenJumpscared && logic.CollectedSkulls() != 5) { // != 5
                jumpscareScript.StartJumpscare();
                hasBeenJumpscared = true;
            }       
            
        // Giocatore uscito dal raggio di visione
        } else if (Vector3.Distance(transform.position, player.position) > warnDistance && sawPlayer) {
            if (audioSource.clip != music1) {
                audioSource.Stop();
                audioSource.clip = music1;
                audioSource.Play();
            }
            
            sawPlayer = false;
            //agent.stoppingDistance = 5;

            WalkRandomly();

            Debug.Log("Player is Gone");
            
        }
    }

    void WalkRandomly() {
        //animator.speed = 0.5f;
        animator.Play("Walking");
        agent.speed = 17f;

        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        // Se la posizione si trova sulla NavMesh va in quella posizione
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, NavMesh.AllAreas)) {
            agent.SetDestination(hit.position);
        }
    }

    /*void Jumpscare() {
        hasBeenJumpscared = true;
        agent.enabled = false;
        enemyCamera.gameObject.SetActive(true);
        audioSource.Stop();
        audioSource.clip = jumpscare;
        animator.Play(scream.name);

        transform.position = enemyCamera.position;
        transform.rotation = enemyCamera.rotation;
        audioSource.Play();

        StartCoroutine(BackToMainScreen(6f));

    }

    private IEnumerator BackToMainScreen(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene("MainMenu");
    }*/


}
