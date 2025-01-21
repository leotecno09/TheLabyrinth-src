using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using Kino;
using UnityEngine.Playables;

public class EndScene : MonoBehaviour
{
    public CinemachineVirtualCamera enemyCam1;
    public CinemachineVirtualCamera enemyDanceCam;
    public CinemachineBrain cinemachineBrain;
    public Animator enemyAnimator;
    public AnimationClip enemyDance;
    public AudioSource enemyAudioSource;
    public AudioClip enemyDanceMusic;
    public AudioClip jumpscareClip;
    public GameObject dissolveScreen;
    private Animation dissolveAnimation;
    private Animator theEndTextAnimator;
    public AnimationClip theEndTextAnimClip;
    public TextMeshProUGUI theEndText;
    public Light directionalEnemyLight;
    public Light playerFlashlight;
    private GameObject theLastSkull;
    public GameObject backToMenuButton;
    private CinemachineVirtualCamera lastSkullCamera;
    public MouseLockManager mouseLockManager;
    public DigitalGlitch digitalGlitch;
    public static bool shouldPlayEndCutscene = false;
    //private bool glitchIncreasing = false;
    // Start is called before the first frame update
    void Start()
    {
        dissolveAnimation = dissolveScreen.GetComponent<Animation>();
        theEndTextAnimator = theEndText.GetComponent<Animator>();
        cinemachineBrain.m_DefaultBlend.m_Time = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEnd(GameObject nearbySkull) {
        mouseLockManager.ToggleMouseLock(false);

        theLastSkull = nearbySkull;
        lastSkullCamera = theLastSkull.GetComponentInChildren<CinemachineVirtualCamera>(true);

        playerFlashlight.gameObject.SetActive(false);
        //dissolveAnimation.Play();

        StartCoroutine(increaseDigitalGlitch());    
    }

    IEnumerator increaseDigitalGlitch() {
        while (digitalGlitch.intensity < 1f) {
            digitalGlitch.intensity += 0.5f * Time.deltaTime;
            digitalGlitch.intensity = Mathf.Clamp(digitalGlitch.intensity, 0f, 1f);
            yield return null;
        }

        StartCoroutine(InvokeSkullCameraAndResetGlitch());
    }

    /*IEnumerator GlitchAndDissolve() {
        yield return new WaitForSeconds(3f);

        dissolveAnimation.Play();

        StartCoroutine(InvokeSkullCameraAndResetGlitch());
    }*/

    IEnumerator InvokeSkullCameraAndResetGlitch() {
        yield return new WaitForSeconds(3f);
        lastSkullCamera.gameObject.SetActive(true);
        digitalGlitch.intensity = 0f;
        //Invoke("EnableSkullCamera", 2f);
        StartCoroutine(TakeSkull());
    }

    /*private void EnableSkullCamera() {
        lastSkullCamera.gameObject.SetActive(true);   // Logicamente quando il teschio viene distrutto la telecamera torna a quella del player, da sistemare!
    }*/

    IEnumerator TakeSkull() {
        yield return new WaitForSeconds(6f);     

        Destroy(theLastSkull);
        theLastSkull = null;

        //cinemachineBrain.m_DefaultBlend.m_Time = 2f;
        StartCoroutine(LookEnemy());

    }

    IEnumerator LookEnemy() {
        yield return new WaitForSeconds(0.7f);

        enemyAudioSource.clip = jumpscareClip;
        enemyCam1.gameObject.SetActive(true);
        digitalGlitch.intensity = 0.3f;
        enemyAudioSource.Play();

        StartCoroutine(MoveToDanceCam());
    }

    IEnumerator MoveToDanceCam() {
        yield return new WaitForSeconds(5f);

        cinemachineBrain.m_DefaultBlend.m_Time = 4f;
        enemyDanceCam.gameObject.SetActive(true);

        StartCoroutine(Dance());
    }

    IEnumerator Dance() {
        yield return new WaitForSeconds(5f);

        digitalGlitch.intensity = 0f;
        enemyAudioSource.clip = enemyDanceMusic;
        enemyAnimator.Play(enemyDance.name);
        enemyAudioSource.Play();
        directionalEnemyLight.gameObject.SetActive(true);

        StartCoroutine(PlayEndCutscene());
    }

    IEnumerator PlayEndCutscene() {
        yield return new WaitForSeconds(15f);

        PlayerPrefs.SetInt("HasAlreadyPlayed", 1);
        PlayerPrefs.Save();

        shouldPlayEndCutscene = true;
        SceneManager.LoadScene("Intro");
    }

    /*IEnumerator ShowTheEndText() {
        yield return new WaitForSeconds(12.3f);

        theEndText.gameObject.SetActive(true);
        theEndTextAnimator.Play(theEndTextAnimClip.name);

        StartCoroutine(StopTheEndText());
    }

    IEnumerator StopTheEndText() {
        yield return new WaitForSeconds(6.58f);

        theEndTextAnimator.enabled = false;

        StartCoroutine(ShowBackButton());
    }

    IEnumerator ShowBackButton() {
        yield return new WaitForSeconds(2.3f);

        backToMenuButton.SetActive(true);
    }

    public void BackToMenu() {
        mouseLockManager.ToggleMouseLock(false);
        SceneManager.LoadScene("MainMenu");
    }*/
}
