using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndGameTrigger : MonoBehaviour
{
    AudioSource whaleAudioSource;
    bool soundBeforeEnd, fadingEnd;
    float startTime, currentTime;
    [Range(0, 10)]
    public float fadingTime = 3;

    float percentToAdd, screenAlpha;

    public Image fadeImage;
    public AudioSource[] ambiantSources;
    float[] maxVolume;

    bool startedEnding;




    // Start is called before the first frame update
    void Start()
    {
        maxVolume = new float[ambiantSources.Length];
        whaleAudioSource = GetComponent<AudioSource>();
        screenAlpha = 0;
        for(int i=0; i<ambiantSources.Length; i++)
        {
            maxVolume[i] = ambiantSources[i].volume;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if((other is CapsuleCollider) && !startedEnding)
        {
            GameEvents.current.BlockPlayerMove(false);
            whaleAudioSource.Play();
            soundBeforeEnd = true;
            startedEnding = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (soundBeforeEnd)
        {
            if (!whaleAudioSource.isPlaying)
            {
                soundBeforeEnd = false;
                fadingEnd = true;
                startTime = Time.time;
            }
        }
        if (fadingEnd)
        {
            currentTime = Time.time;

            percentToAdd = Time.deltaTime / fadingTime;
            for (int i = 0; i < ambiantSources.Length; i++)
            {
                ambiantSources[i].volume -= percentToAdd * maxVolume[i];
            }


            screenAlpha += percentToAdd;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, screenAlpha);

            if (currentTime - startTime >= fadingTime)
            {
                fadingEnd = false;
                screenAlpha = 1;
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, screenAlpha);
                for (int i = 0; i < ambiantSources.Length; i++)
                {
                    ambiantSources[i].volume = 0;
                    ambiantSources[i].Stop();
                }

                SceneManager.LoadScene("Menu");
            }
        }
    }
}
