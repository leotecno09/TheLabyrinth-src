using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Kino;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera jumpscareCamera;
    public DigitalGlitch digitalGlitch;
    // Start is called before the first frame update
    void Start()
    {
        //jumpscareCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator JumpscareShake(float intensity) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            jumpscareCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = intensity;

        digitalGlitch.intensity = 0.2f;

        yield return null;
    }
}
