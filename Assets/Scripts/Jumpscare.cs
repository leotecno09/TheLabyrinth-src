using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Jumpscare : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera enemyCamera;
    public CinemachineBrain cinemachineBrain;
    public NavMeshAgent enemyNavMesh;
    public AudioSource audioSource;
    public AudioClip jumpscare;
    public Animator enemyAnimator;
    public AnimationClip scream;
    public MouseLockManager mouseLockManager;
    public CameraShake cameraShake;
    public ChangeAmbientIntensity changeAmbientIntensity;
    //public float lerpDuration = 1.0f;
    //private float lerpTime = 0f;
    //private bool isDoingAnim = false;
    //private Vector3 startPos;
    //private Quaternion startRot;
    //private Vector3 endPos;
    //private Quaternion endRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isDoingAnim) {
            lerpTime += Time.deltaTime;
            float t = lerpTime / lerpDuration;

            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);

            if (lerpTime >= lerpDuration) {
                isDoingAnim = false;
            }
        }*/
    }

    public void StartJumpscare() {
        cinemachineBrain.m_DefaultBlend.m_Time = 0.35f;
        enemyNavMesh.enabled = false;
        enemyCamera.gameObject.SetActive(true);

        audioSource.Stop();
        audioSource.clip = jumpscare;

        //enemyAnimator.Play(scream.name);
        enemyAnimator.enabled = false;

        //playerCamera.enabled = true;
        enemyCamera.enabled = true;
        StartCoroutine(cameraShake.JumpscareShake(8f));
        //changeAmbientIntensity.ChangeColorIntensity();

        audioSource.Play();

        StartCoroutine(BackToMainMenu(5f));
    }

    private IEnumerator BackToMainMenu(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        //mouseLockManager.ToggleMouseLock(false);

        SceneManager.LoadScene("SampleScene");
    }
}
