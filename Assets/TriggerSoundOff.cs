using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundOff : MonoBehaviour
{
            
    public GameObject gameObj;
    public AudioSource source;
    bool didStartDecrease;


    float baseValue;
    [Range(0, 1)]
    public float minVolume = 0;
    [Range(0, 1f)]
    public float decreaseValue = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        baseValue = source.volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            if (!didStartDecrease)
            {
                didStartDecrease = true;
                source = gameObj.GetComponent<AudioSource>();
                gameObj.GetComponent<TriggerSoundOn>().enabled = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (didStartDecrease)
        {
            if (source.volume <= minVolume)
            {
                source.volume = minVolume;
                source.Stop();
            }
            else
                source.volume -= decreaseValue * Time.deltaTime;
        }
    }

}
