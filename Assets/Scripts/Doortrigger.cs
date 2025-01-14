﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doortrigger : MonoBehaviour
{
    public bool isInZone = false;
    Animator button;
    public Animator porte;
    public Animator fenetre;
    AudioSource levierAudio;
    public AudioSource doorAudio;
    bool didOpenDoor;

    // etat de l'animation
    enum animationState
    {
        started = 0,
        inPause = 1,
        ended = 2
    };

    void Start()
    {
        button = GetComponent<Animator>();
        levierAudio = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider col)
    {
        isInZone = true;
    }
    void OnTriggerExit(Collider col)

    {
        isInZone = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isInZone && !didOpenDoor)
        {
            didOpenDoor = true;
            button.SetBool("activate", true);
            levierAudio.Play();
            GameEvents.current.BlockPlayerMove(false);
        }

    }

    // L'animation envoie un message quand elle est terminée
    public void AlertObserver(int message)
    {
        if(message == (int)animationState.ended)
        {
            porte.SetBool("isRotating", true);
            doorAudio.Play();
            GameEvents.current.BlockPlayerMove(true);

        }
    }
}

