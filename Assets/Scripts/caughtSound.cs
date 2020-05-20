using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caughtSound : MonoBehaviour
{
    AudioSource source;

    public bool seen;
    bool firstTimeSeen;

    float baseValue;
    [Range(0, 1)]
    public float maxVolume = 1;
    [Range(0, 1f)]
    public float growthValue = 0.1f;
    [Range(0, 1f)]
    public float decreaseValue = 0.1f;

    // When dead
    bool isDead;
    float deadDuration, percentToAdd;
    float startTime, currentTime, deltaTime;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        baseValue = source.volume;
        GameEvents.current.onBeingSeen += OnGettingCaughtSound;
        GameEvents.current.onDying += StopSound;
    }

    void OnGettingCaughtSound(bool caught)
    {
        seen = caught && !isDead;
    }


    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            currentTime = Time.time;
            deltaTime = currentTime - startTime;

            percentToAdd = Time.deltaTime / deadDuration;
            source.volume -= percentToAdd * (maxVolume - baseValue);

            if(deltaTime >= deadDuration)
            {
                source.volume = baseValue;
                source.Stop();
                firstTimeSeen = false;
                isDead = false;
            }
        }
        else
        {
            if (seen)
            {
                if (!firstTimeSeen)
                {
                    firstTimeSeen = true;
                    source.Play();
                }
                if (source.volume >= maxVolume)
                    source.volume = maxVolume;
                else
                    source.volume += growthValue * Time.deltaTime;
            }
            else
            {
                if (source.volume <= 0.1f)
                {
                    source.volume = baseValue;
                    firstTimeSeen = false;
                    source.Stop();
                }
                else
                    source.volume -= decreaseValue * Time.deltaTime;
            }
        }
        
    }

    private void StopSound(float roomNumber, float respawnDuration)
    {
        isDead = true;
        seen = false;
        deadDuration = respawnDuration;
        startTime = currentTime = Time.time;
    }
}
