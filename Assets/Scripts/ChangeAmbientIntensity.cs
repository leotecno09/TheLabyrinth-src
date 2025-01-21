using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmbientIntensity : MonoBehaviour
{
    public float minIntensity = -10f;
    public float maxIntensity = 5f;
    public float glitchDuration = 0.5f;
    public float glitchFrequency = 1.0f;
    private bool isGlitching;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColorIntensity() {
        StartCoroutine(GlitchEffect());
    }

    private IEnumerator GlitchEffect() {
        while (true) {
            float nextGlitchTime = Random.Range(0.1f, glitchFrequency);
            yield return new WaitForSeconds(nextGlitchTime);

            isGlitching = true;
            float originalIntensity = RenderSettings.ambientIntensity;

            for (float t = 0; t < glitchDuration; t += Time.deltaTime) {
                RenderSettings.ambientIntensity = Random.Range(minIntensity, maxIntensity);
                yield return null;
            }

            RenderSettings.ambientIntensity = originalIntensity;
            isGlitching = false;
        }
    }
}
