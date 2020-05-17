using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StreamVideo : MonoBehaviour
{
    public RawImage rawimage;
    public VideoPlayer videoplayer;
    

    void Start()
    {
        StartCoroutine(Playvideo());
        videoplayer = gameObject.AddComponent<VideoPlayer>();
        videoplayer.playOnAwake = false;
        videoplayer.source = VideoSource.VideoClip;
    }
    IEnumerator Playvideo()
    {

        videoplayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.01f);
        while (!videoplayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        rawimage.texture = videoplayer.texture;
        videoplayer.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        
    }
   


}
