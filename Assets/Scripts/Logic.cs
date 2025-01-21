using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Cinemachine;
//using UnityEngine.Playables;
public class Logic : MonoBehaviour
{
    private GameObject nearbySkull;
    public TextMeshProUGUI collectText;
    public TextMeshProUGUI readText;
    public TextMeshProUGUI skullCounter;
    public TextMeshProUGUI poeticText;
    public GameObject textBox;
    public EndScene endScript;
    //public PlayableDirector playableDirector;
    //public PlayableAsset endTimeline;
    //public UnityEngine.UI.Image dissolve;

    public GameObject enemy;
    //public GameObject[] tpPoints;
    //public Transform enemyTr;
    public FollowPlayer followPlayerScript;
    
    public Animator enemyAnimator;

    public AnimationClip idleAnimation;

    public NavMeshAgent enemyNavMeshAgent;

    //public Transform cameraTransform;
    public Transform player;
    public AudioSource audioSource;
    public AudioSource playerAudioSource;
    public AudioClip skullTakenSound;
    public AudioClip paperSound;

    //public CinemachineVirtualCamera virtualCamera;

    private int collectedSkulls = 0;
    private bool isEnd = false;
    private bool isNearPaperSheet = false;
    private bool textBoxIsOpened = false;
    public PlayerMovement playerMovementScript;
    public MouseLook mouseLookScript;
    //public MouseLockManager mouseLockManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (nearbySkull && !isEnd && Input.GetMouseButton(0)) {
            CollectItem();
        }

        if (isNearPaperSheet && Input.GetMouseButton(0)) {

            bool hasAlreadyPlayed = PlayerPrefs.GetInt("HasAlreadyPlayed", 0) == 1;

            if (hasAlreadyPlayed) {
                poeticText.text = "I have already seen you...";
            }

            playerAudioSource.clip = paperSound;
            playerAudioSource.Play();

            textBox.gameObject.SetActive(true);
            readText.gameObject.SetActive(false);

            mouseLookScript.enabled = false;
            playerMovementScript.enabled = false;

            textBoxIsOpened = true;
        }

        if (textBoxIsOpened && Input.GetKeyDown(KeyCode.Escape)) {
            textBox.gameObject.SetActive(false);
            textBoxIsOpened = false;

            mouseLookScript.enabled = true;
            playerMovementScript.enabled = true;

            //mouseLockManager.ToggleMouseLock(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Skull")) {
            nearbySkull = other.gameObject;
            collectText.gameObject.SetActive(true);

            Debug.Log("Compaaaa sei vicino ad un teschh");
        } else if (other.CompareTag("PaperSheet")) {
            readText.gameObject.SetActive(true);
            isNearPaperSheet = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Skull")) {
            nearbySkull = null;
            collectText.gameObject.SetActive(false);
        } else if (other.CompareTag("PaperSheet")) {
            readText.gameObject.SetActive(false);
            isNearPaperSheet = false;
        }     
    }

    private void CollectItem() {
        collectText.gameObject.SetActive(false);
        collectedSkulls++;

        if (collectedSkulls == 5) {
            isEnd = true;
            audioSource.Stop();
            enemyNavMeshAgent.enabled = false;
            followPlayerScript.enabled = false;
            skullCounter.enabled = false;
            //dissolve.enabled = true;
            //enemy.transform.position = new Vector3(this.transform.position.x - 10, this.transform.position.y, this.transform.position.z);
            Vector3 directionBehindPlayer = -player.transform.forward;
            Vector3 targetPos = player.transform.position + directionBehindPlayer * 8; // 8 è la distanza

            Ray ray = new Ray(targetPos + Vector3.up * 10, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 20f)) {    // Enemy non toccava il terreno
                targetPos = hitInfo.point;
            }

            enemy.transform.position = targetPos;   // Se un player bastardo va attaccato al muro l'enemy si tippa dentro il muro
            enemy.transform.rotation = player.transform.rotation;


            enemyAnimator.Play(idleAnimation.name);

            endScript.PlayEnd(nearbySkull);

            //enemy.transform.rotation = player.rotation;
            //playableDirector.Play();



        } else {
            Destroy(nearbySkull);
            nearbySkull = null;

            playerAudioSource.clip = skullTakenSound;
            playerAudioSource.Play();


            //collectedSkulls++;
            skullCounter.SetText($"SKULLS: {collectedSkulls}/5");

            /*Debug.Log("Player Position: X = " + this.transform.position.x + " --- Y = " + this.transform.position.y + " --- Z = " + 
            this.transform.position.z);*/
        }
    }

    public int CollectedSkulls() {
        return collectedSkulls;
    }

    public bool IsEnd() {
        return isEnd;
    }

    /*bool IsColliderFree(BoxCollider collider) {
        Vector3 center = collider.transform.position + collider.center;
        Vector3 size = collider.size * 0.5f;

        Collider[] hitColliders = Physics.OverlapBox(center, size, collider.transform.rotation);

        foreach (Collider hitCollider in hitColliders) {
            if (hitCollider.gameObject != collider.gameObject) {
                return false;
            }
        }

        return true;
    }*/

    /*IEnumerator PerformCameraAnimation() {
        Vector3 startPosition = cameraTransform.localPosition;
        Vector3 targetPosition = new Vector3(0, -1, -5); // Accovacciamento

        float elapsedTime = 0f;

        while (elapsedTime < 2f) {
            cameraTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = targetPosition;

        yield return new WaitForSeconds(2.5f);

        //Quaternion startRotation = cameraTransform.rotation;
        Vector3 enemyFacePosition = enemyTr.position + enemyTr.forward * 10;
        Quaternion targetRotation = Quaternion.Euler(30f, cameraTransform.rotation.eulerAngles.y, cameraTransform.rotation.eulerAngles.z);
        elapsedTime = 0f;

        while (elapsedTime < 1.5f) {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, enemyFacePosition, elapsedTime / 1.5f);
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, elapsedTime / 1.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = enemyFacePosition;
        cameraTransform.rotation = targetRotation;
    }*/

    /*private void ShowCollectText(bool show) {
        collectText.enabled = show;
    }*/
}
