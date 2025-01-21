using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameScene() {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadIntroScene() {
        SceneManager.LoadScene("Intro");
    }

    public void LoadEndCredits() {
        SceneManager.LoadScene("EndCredits");
    }
}
