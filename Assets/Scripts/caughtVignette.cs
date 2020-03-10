using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class caughtVignette : MonoBehaviour
{
    
    PostProcessProfile profile;
    Vignette vignette;

    public bool seen;

    float baseValue;
    [Range(0, 1)]
    public float maxValue = 1;
    [Range(0, 1f)]
    public float growthValue = 0.1f;
    [Range(0, 1f)]
    public float decreaseValue = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        profile = GetComponent<PostProcessVolume>().profile;
        vignette = profile.GetSetting<Vignette>();
        baseValue = vignette.intensity.value;
        GameEvents.current.onBeingSeen += OnGettingCaughtVignette;
    }

    void OnGettingCaughtVignette(bool caught)
    {
        seen = caught;
    }

    // Update is called once per frame
    void Update()
    {

        if (seen)
        {
            if (vignette.intensity.value >= 1)
                vignette.intensity.value = 1;
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
