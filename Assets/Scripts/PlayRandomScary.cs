using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomScary : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] scarySounds;
    private AudioClip selectedSound;
    public Logic logicScript;
    private int randomInterval;
    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying && !logicScript.IsEnd()) {
            randomInterval = Random.Range(30, 70);
            Invoke("PlayRandomSound", randomInterval);
            Debug.Log($"Suono random tra: {randomInterval}");
            isPlaying = true;
        }

    }

    void PlayRandomSound() {
        if (!logicScript.IsEnd()) {
            int randomIndex = Random.Range(0, scarySounds.Length);

            selectedSound = scarySounds[randomIndex];

            audioSource.clip = selectedSound;
            audioSource.Play();

            isPlaying = false;
        }

    }
}
