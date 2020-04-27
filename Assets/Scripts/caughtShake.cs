using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class caughtShake : MonoBehaviour
{
    public bool seen;

    float baseValue = 0;
    public float maxValue = 1;
    [Range(0, 1f)]
    public float growthValue = 0.4f;
    [Range(0, 1f)]
    public float decreaseValue = 0.8f;

    CinemachineBasicMultiChannelPerlin screenShake;

    // on Death
    bool isDead;
    float startTime, currentTime, deltaTime;
    float percentToAdd, deadDuration;

    // Start is called before the first frame update
    void Start()
    {
        screenShake = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GameEvents.current.onBeingSeen += OnGettingCaughtScreenShake;
        GameEvents.current.onDying += StopShake;
    }

    void OnGettingCaughtScreenShake(bool caught)
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
            screenShake.m_AmplitudeGain -= percentToAdd * (maxValue - baseValue);
            screenShake.m_FrequencyGain -= percentToAdd * (maxValue - baseValue);

            if (deltaTime >= deadDuration)
            {
                screenShake.m_AmplitudeGain = baseValue;
                screenShake.m_FrequencyGain = baseValue;
                isDead = false;
            }
        }
        else
        {
            if (seen)
            {
                if (screenShake.m_AmplitudeGain >= maxValue)
                    screenShake.m_AmplitudeGain = maxValue;
                else
                    screenShake.m_AmplitudeGain += growthValue * Time.deltaTime;

                if (screenShake.m_FrequencyGain >= maxValue)
                    screenShake.m_FrequencyGain = maxValue;
                else
                    screenShake.m_FrequencyGain += growthValue * Time.deltaTime;
            }
            else
            {
                if (screenShake.m_AmplitudeGain <= baseValue)
                    screenShake.m_AmplitudeGain = baseValue;
                else
                    screenShake.m_AmplitudeGain -= decreaseValue * Time.deltaTime;

                if (screenShake.m_FrequencyGain <= baseValue)
                    screenShake.m_FrequencyGain = baseValue;
                else
                    screenShake.m_FrequencyGain -= decreaseValue * Time.deltaTime;
            }
        }
    }


    private void StopShake(float roomNumber, float respawnDuration)
    {
        isDead = true;
        seen = false;
        deadDuration = respawnDuration;
        startTime = currentTime = Time.time;
    }
}
