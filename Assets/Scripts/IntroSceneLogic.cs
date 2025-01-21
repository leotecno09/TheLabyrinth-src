using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class IntroSceneLogic : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableAsset endCustsceneTimeline;
    // Start is called before the first frame update
    void Start()
    {
        if (EndScene.shouldPlayEndCutscene) {
            playableDirector.playableAsset = endCustsceneTimeline;
            playableDirector.Play();

            EndScene.shouldPlayEndCutscene = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
