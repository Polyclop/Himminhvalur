using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class caughtVignette : MonoBehaviour
{
    VolumeProfile profile;
    Vignette vignette;

    public bool seen;

    float baseValue;
    [Range(0, 1)]
    public float maxValue = 1;
    [Range(0, 1f)]
    public float growthValue = 0.1f;
    [Range(0, 1f)]
    public float decreaseValue = 0.1f;

    public float vignetteIntensity;

    // on Death
    bool isDead;
    float startTime, currentTime, deltaTime;
    float percentToAdd, deadDuration;

    // Start is called before the first frame update
    void Start()
    {
        profile = GetComponent<Volume>().profile;
        Vignette gnette;
        if (profile.TryGet<Vignette>(out gnette)){
            vignette = gnette;
        }
        baseValue = vignette.intensity.value;
        GameEvents.current.onBeingSeen += OnGettingCaughtVignette;
        GameEvents.current.onDying += StopVignette;
    }

    void OnGettingCaughtVignette(bool caught)
    {
        seen = caught && !isDead;
    }

    // Update is called once per frame
    void Update()
    {
        vignetteIntensity = vignette.intensity.value;
        if (isDead)
        {
            currentTime = Time.time;
            deltaTime = currentTime - startTime;

            percentToAdd = Time.deltaTime / deadDuration;
            vignette.intensity.value -= percentToAdd * (maxValue - baseValue);

            if (deltaTime >= deadDuration)
            {
                vignette.intensity.value = baseValue;
                isDead = false;
            }
        }
        else
        {
            if (seen)
            {
                if (vignette.intensity.value >= maxValue)
                    vignette.intensity.value = maxValue;
                else
                    vignette.intensity.value += growthValue * Time.deltaTime;
            }
            else
            {
                if (vignette.intensity.value <= baseValue)
                    vignette.intensity.value = baseValue;
                else
                    vignette.intensity.value -= decreaseValue * Time.deltaTime;
            }
        }
        
    }


    private void StopVignette(float roomNumber, float respawnDuration)
    {
        isDead = true;
        seen = false;
        deadDuration = respawnDuration;
        startTime = currentTime = Time.time;
    }
}
