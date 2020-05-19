using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundTrigger : MonoBehaviour
{
    AudioSource targetAudio;
    public bool playOnce =  true;
    bool didPlay;

    // Start is called before the first frame update
    void Start()
    {
        targetAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!playOnce || (playOnce && !didPlay))
        {
            targetAudio.Play();
        }
    }
}
