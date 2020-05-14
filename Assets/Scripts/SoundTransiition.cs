using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTransiition : MonoBehaviour
{
    public AudioMixerSnapshot salle3Enter;
    public AudioMixerSnapshot salle3Exit;

   AudioSource audiosource;
    

    Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
        audiosource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
       
        salle3Enter.TransitionTo(2f);
        audiosource.Play(0);
       

    }

    private void OnTriggerExit(Collider other)
    { 
        
        
        salle3Exit.TransitionTo(7.0f);

    }


}
