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
    public float growthValue = 0.1f;
    [Range(0, 1f)]
    public float decreaseValue = 0.1f;

    CinemachineBasicMultiChannelPerlin screenShake;

    // Start is called before the first frame update
    void Start()
    {
        screenShake = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        GameEvents.current.onBeingSeen += OnGettingCaughtScreenShake;
    }

    void OnGettingCaughtScreenShake(bool caught)
    {
        seen = caught;
    }

    // Update is called once per frame
    void Update()
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
