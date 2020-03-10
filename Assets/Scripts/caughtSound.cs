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

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        baseValue = source.volume;
        GameEvents.current.onBeingSeen += OnGettingCaughtSound;
    }

    void OnGettingCaughtSound(bool caught)
    {
        seen = caught;
    }


    // Update is called once per frame
    void Update()
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
            if (source.volume <= baseValue)
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
