using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLight : MonoBehaviour
{
    public AudioClip soundClip;
    private AudioSource audioSource;

    public Light light; 
    private bool lightOn = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (soundClip != null) {
            audioSource.clip = soundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (lightOn) {
                light.gameObject.SetActive(false);
                lightOn = false;
                audioSource.Play();
            } else {
                light.gameObject.SetActive(true);
                lightOn = true;
                audioSource.Play();
            }
        }
    }
}
