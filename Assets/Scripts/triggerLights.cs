using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerLights : MonoBehaviour
{
    bool shallTriggerLights;
    public Light lit;
    float maxIntensity;
    public float intensityIncrease = 0.01f;
    public caughtJauge jauge;
    // Start is called before the first frame update
    void Start()
    {
        maxIntensity = lit.intensity;
        lit.intensity = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider && !shallTriggerLights)
        {
            shallTriggerLights = true;
            jauge.seen = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shallTriggerLights && lit.intensity < maxIntensity)
        {
            lit.intensity += intensityIncrease;
        }
    }
}
