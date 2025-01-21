using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChangeVideo : MonoBehaviour
{
    public VideoPlayer screenVideo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVideoClip(VideoClip newVideo) {
        screenVideo.clip = newVideo;
        screenVideo.Play();
    }
}
