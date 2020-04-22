using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundOn : MonoBehaviour
{
    AudioSource source;

    bool didPlay;

    float baseValue;
    [Range(0, 1)]
    public float maxVolume = 1;
    [Range(0, 1f)]
    public float growthValue = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        baseValue = source.volume;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other is CapsuleCollider)
        {
            if (!didPlay)
            {
                didPlay = true;
                source.Play();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (didPlay)
        {
            if (source.volume >= maxVolume)
                source.volume = maxVolume;
            else
                source.volume += growthValue * Time.deltaTime;
        }    }
}
