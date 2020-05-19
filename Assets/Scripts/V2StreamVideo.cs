using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Rewired;

public class V2StreamVideo : MonoBehaviour
{
    public RawImage image;
    public VideoClip videoToPlay;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource audioSource;

    float startTime, currentTime;
    bool startedTimer;

    int playerID = 0;
    Player player;

    // Use this for initialization



    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
        videoPlayer.loopPointReached += EndReached;
        player = ReInput.players.GetPlayer(playerID);

    }

    private void Update()
    {

        if (player.GetAnyButtonDown())
        {
            SceneManager.LoadScene("Menu");

        }
    }

    IEnumerator playVideo()
    {
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();
        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.VideoClip;
        // Vide clip from Url
        //videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";
        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();
        //Wait until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            image.color = Color.black;
            //Prepare/Wait for 5 sceonds only
            yield return waitTime;
            //Break out of the while loop after 5 seconds wait
            break;
        }
        Debug.Log("Done Preparing Video");
        image.color = Color.white;
        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;
        //Play Video
        videoPlayer.Play();
        //Play Sound
        audioSource.Play();
        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }
        Debug.Log("Done Playing Video");
        videoPlayer.Stop();
        SceneManager.LoadScene("Menu");
    }


    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //videoPlayer.Stop();
        //SceneManager.LoadScene("Menu");

    }
}

