using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour
{
    public Canvas loadingScreen;
    private AudioSource audioSource;
    public AudioClip intro;
    public AudioClip loop;
    //private bool introPlayed = false;
    public GameObject aboutPanel;
    public MouseLockManager mouseLockManager;
    //public PlayableDirector playableDirector;
    //public double loopStartTime = 30.6667;
    //public double loopEndTime = 46.62;
    //public Loader loader;
    //public Loader loader;
    // Start is called before the first frame update
    void Start()
    {
        mouseLockManager.ToggleMouseLock(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;

        PlayIntro();

    }

    // Update is called once per frame
    void Update()
    {
    }

    void PlayIntro() {
        audioSource.clip = intro;
        audioSource.Play();

        Invoke(nameof(PlayLoop), intro.length - 0.48f);
    }

    void PlayLoop() {
        audioSource.clip = loop;
        audioSource.loop = true;

        audioSource.Play();
    }

    public void StartGame() {
        audioSource.Stop();

        //SceneManager.LoadScene("SampleScene");
        //loader.Load("SampleScene");
        //Invoke("LoadGameScene", 1f);
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ShowAbout() {
        aboutPanel.SetActive(true);
    }

    public void CloseAboutPanel() {
        aboutPanel.gameObject.SetActive(false);

        //Debug.Log("CLLOSING THE FAVA OF PANEL OF ABOUT");
    }
}
