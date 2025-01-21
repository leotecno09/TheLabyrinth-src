using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class DetectTape : MonoBehaviour
{
    public bool playerHasTape = false;
    private bool isLookingVhsTape = false;
    private bool isLookingVhsPlayer = false;
    public TextMeshProUGUI takeVhsText;
    public TextMeshProUGUI playText;
    public GameObject vhsTape;
    public PlayableDirector introTimeline;
    // Start is called before the first frame update
    void Start()
    {
        takeVhsText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLookingVhsPlayer && Input.GetKeyDown(KeyCode.P)) {
            playerHasTape = false;
            isLookingVhsPlayer = false;
            playText.gameObject.SetActive(false);
            introTimeline.Play();
        }

        if (isLookingVhsTape && Input.GetKeyDown(KeyCode.E)) {
            playerHasTape = true;
            isLookingVhsTape = false;
            vhsTape.SetActive(false);
            takeVhsText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("VHSTape")) {
            takeVhsText.gameObject.SetActive(true);
            isLookingVhsTape = true;
        } else if (other.CompareTag("VHSPlayer")) {
            if (playerHasTape) {
                playText.gameObject.SetActive(true);
                isLookingVhsPlayer = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("VHSTape")) {
            takeVhsText.gameObject.SetActive(false);
            isLookingVhsTape = false;
        } else if (other.CompareTag("VHSPlayer")) {
            if (playerHasTape) {
                isLookingVhsPlayer = false;
                playText.gameObject.SetActive(false);
            }
        }
    }
}
