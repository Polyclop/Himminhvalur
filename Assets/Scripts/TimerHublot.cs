using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHublot : MonoBehaviour
{
    float startTime, currentTime, deltaTime;
    [Range(0,10)] public float countdown = 10;
    public bool isStarted;
    Animator hublotAnimator;
    public Animator levierAnimator;
    BoxCollider lightTrigger;
    public AudioClip ouvertureAudio;
    public AudioClip fermetureAudio;
    AudioSource voletAudio;


    private void Start()
    {
        hublotAnimator = GetComponent<Animator>();
        lightTrigger = GetComponentInParent<BoxCollider>();
        voletAudio = GetComponent<AudioSource>();
        voletAudio.clip = (Random.Range(0, 2) == 0) ? fermetureAudio : ouvertureAudio;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            HandleTimer();
           
        }
        
    }

    void HandleTimer()
    {

        currentTime = Time.time;
        deltaTime = currentTime - startTime;
        

        if (deltaTime >= countdown)
        {
            isStarted = false;

            OpenHublot();

        }

        
    }
    public void LaunchTimer()
    {
        isStarted = true;
        startTime = Time.time;
        lightTrigger.enabled = false;

    }
    void OpenHublot()
    {
        hublotAnimator.SetBool("isRotating", false);
        levierAnimator.SetBool("activate", false);
        lightTrigger.enabled = true;
        voletAudio.clip = (Random.Range(0, 2) == 0) ? fermetureAudio : ouvertureAudio;
        voletAudio.Play();
        
       
       


    }




}


