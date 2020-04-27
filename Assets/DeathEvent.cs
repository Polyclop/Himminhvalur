using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathEvent : MonoBehaviour
{
    Image blackScreen;
    float screenAlpha;
    [Range(0, 10)]
    public float fadeInDuration = 3;
    [Range(0, 10)]
    public float waitDuration = 1;
    [Range(0, 10)]
    public float fadeOutDuration = 2;
    
    bool startedFadeIn, startedWait, startedFadeOut;
    float startTime, currentTime, deltaTime;
    float percentToAdd;

    float currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        //GameEvents.current.onDying += onDeathEvent;
        GameOverCheck.deathDelegate += StartDeathEvent;
        GameEvents.current.onChangingRoom += GetRoomForSpawnpoint;

        blackScreen = GetComponent<Image>();

    }

    private void Update()
    {
        currentTime = Time.time;
        deltaTime = currentTime - startTime;
        if (startedFadeIn)
        {

            percentToAdd = Time.deltaTime / fadeInDuration;
            screenAlpha += percentToAdd;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, screenAlpha);
            if (deltaTime >= fadeInDuration)
            {
                LaunchRepopProcess();
            }
        }
        if (startedWait)
        {
            if (deltaTime >= waitDuration)
            {
                BeginFadeOut();
            }
        }
        if (startedFadeOut)
        {
            percentToAdd = Time.deltaTime / fadeOutDuration;
            screenAlpha -= percentToAdd;
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, screenAlpha);
            if (deltaTime >= fadeOutDuration)
            {
                startedFadeOut = false;
                screenAlpha = 0;
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, screenAlpha);

            }
        }
    }

    private void StartDeathEvent()
    {
        startedFadeIn = true;
        startTime = Time.time;
        screenAlpha = 0;

    }

    private void LaunchRepopProcess()
    {
        startedFadeIn = false;
        startedWait = true;
        startTime = Time.time;
        currentTime = Time.time;
        deltaTime = currentTime - startTime;
        screenAlpha = 1;
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, screenAlpha);
        GameEvents.current.Die(currentRoom, waitDuration);
        
    }

    private void BeginFadeOut()
    {
        startedWait = false;
        startedFadeOut = true;
        startTime = Time.time;
        currentTime = Time.time;
        deltaTime = currentTime - startTime;
    }

    private void GetRoomForSpawnpoint(float room)
    {
        currentRoom = room;
    }
}
