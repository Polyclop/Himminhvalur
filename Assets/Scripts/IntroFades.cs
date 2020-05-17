using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroFades : MonoBehaviour
{
    Image[] introImages;
    float screenAlpha;
    [Range(0, 10)]
    public float fadeInDuration = 3;
    [Range(0, 10)]
    public float waitDuration = 1;
    [Range(0, 10)]
    public float fadeOutDuration = 2;

    bool startedFadeIn, startedWait, startedFadeOut;
    float startTime, currentTime, deltaTime;
    float percentToAdd;


    AudioSource ambiantSource;

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {

        introImages = GetComponents<Image>();
        ambiantSource = GetComponent<AudioSource>();
        screenAlpha = 0;
        introImages[i].color = new Color(introImages[i].color.r, introImages[i].color.g, introImages[i].color.b, screenAlpha);

        // Au début on commence sur du noir et on fade out
        if (ambiantSource != null)
            ambiantSource.Play();
        StartFadeOut();
    }

    private void Update()
    {
        currentTime = Time.time;
        deltaTime = currentTime - startTime;

        if (startedFadeIn)
        {
            HandleFadeIn();
        }

        if (startedFadeOut)
        {
            HandleFadeOut();
        }

        if (startedWait)
        {
            //quand on a finin d'attendre on fadeOut;
            if (deltaTime >= waitDuration)
            {
                StartFadeOut();
            }
        }
    }

    private void StartFadeIn()
    {
        startedFadeIn = true;
        startTime = Time.time;
        screenAlpha = 0;
    }

    private void HandleFadeIn()
    {
        percentToAdd = Time.deltaTime / fadeInDuration;
        screenAlpha += percentToAdd;
        introImages[i].color = new Color(introImages[i].color.r, introImages[i].color.g, introImages[i].color.b, screenAlpha);

        //Quand on a fini le fade in on attend un peu si on veut
        if (deltaTime >= fadeInDuration)
        {
            StartWait();
        }
    }



    private void StartFadeOut()
    {
        startedWait = false;
        startedFadeOut = true;
        startTime = Time.time;
        currentTime = Time.time;
        deltaTime = currentTime - startTime;


    }

    private void HandleFadeOut()
    {
        percentToAdd = Time.deltaTime / fadeOutDuration;
        screenAlpha -= percentToAdd;
        introImages[i].color = new Color(introImages[i].color.r, introImages[i].color.g, introImages[i].color.b, screenAlpha);
        if (deltaTime >= fadeOutDuration)
        {
            startedFadeOut = false;
            screenAlpha = 0;
            introImages[i].color = new Color(introImages[i].color.r, introImages[i].color.g, introImages[i].color.b, screenAlpha);

            // Quand le fadeOut est fini on attend que le joeur appuie sur un bouton
            WaitForInput();
        }
    }


    private void StartWait()
    {
        startedFadeIn = false;
        startedWait = true;
        startTime = Time.time;
        currentTime = Time.time;
        deltaTime = currentTime - startTime;
        screenAlpha = 1;
        introImages[i].color = new Color(introImages[i].color.r, introImages[i].color.g, introImages[i].color.b, screenAlpha);


        // On change d'image avant de faire le fadeOut
        i++;

        // Si on a vu toutes les images
        if (i > introImages.Length)
        {
            //changer de scene
        }

    }

    private void WaitForInput()
    {
        // Si on appuie sur un bouton on fadeIn
        if (Input.anyKeyDown)
        {
            StartFadeIn();
        }
    }

}
