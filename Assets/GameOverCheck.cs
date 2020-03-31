﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCheck : MonoBehaviour
{
    public bool seen;
    bool deathTimerStarted, lifeTimerStarted;
    [Range(0, 20f)]
    public float timeBeforeDeath;
    float startTimeDeath, startTimeLife, currentTime;
    float deltaTime;
    float currentSeenTime = 0;
    float lastMaxSeenTime = 0;
    float lastMaxSafeTime = 0;
    Text gameOverText;

    public float whatIsMyTime;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onBeingSeen += OnGettingCaughtGameOver;
        gameOverText = GetComponent<Text>();
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        if (seen)
        {
            // if just got caught start Death Timer
            if (!deathTimerStarted)
            {
                deathTimerStarted = true;
                lifeTimerStarted = false;
                startTimeDeath = currentTime = Time.time;
            }
            // Check for death
            deltaTime = currentTime - startTimeDeath;
            currentSeenTime = lastMaxSafeTime + deltaTime;
            lastMaxSeenTime = currentSeenTime;
            
            if(currentSeenTime >= timeBeforeDeath)
            {
                //youdie
                
                gameOverText.enabled = true;
            }
        }
        // if safe calm down
        else
        {
            if (currentSeenTime > 0)
            {
                if (!lifeTimerStarted)
                {
                    deathTimerStarted = false;
                    lifeTimerStarted = true;
                    startTimeLife = currentTime = Time.time;
                }
                // Check for life
                deltaTime = currentTime - startTimeLife;
                currentSeenTime = lastMaxSeenTime - deltaTime;
                lastMaxSafeTime = currentSeenTime;

                if (currentSeenTime <= timeBeforeDeath)
                {
                    //youdienot

                    gameOverText.enabled = false;
                }

                if (currentSeenTime <= 0)
                {
                    //yousafe
                    deathTimerStarted = false;
                    lifeTimerStarted = false;
                    currentSeenTime = lastMaxSeenTime = 0;
                    
                }
            }
            
        }

        whatIsMyTime = ((float)((int)(10 * currentSeenTime)) / 10);
    }

    void OnGettingCaughtGameOver(bool caught)
    {
        seen = caught;
    }
}
