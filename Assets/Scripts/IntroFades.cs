using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class IntroFades : MonoBehaviour
{
    public Sprite[] introSprite;

    Image introImage;
    public float screenAlpha;
    [Range(0, 10)]
    public float fadeInDuration = 3;
    [Range(0, 10)]
    public float waitDuration = 1;
    [Range(0, 10)]
    public float fadeOutDuration = 2;

    bool startedFadeIn, startedWait, startedFadeOut;
    float startTime, currentTime, deltaTime;
    float percentToAdd;
    bool isWaitingForInput;


    AudioSource ambiantSource;

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {

        
        ambiantSource = GetComponent<AudioSource>();
        introImage = GetComponent<Image>();
        screenAlpha = 0;
        introImage.sprite = introSprite[i];
        introImage.color = new Color(introImage.color.r, introImage.color.g, introImage.color.b, screenAlpha);

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

                // Si on a vu toutes les images
                if (i >= introSprite.Length)
                {
                    SceneManager.LoadScene("Main");
                }

                StartFadeOut();
            }
        }
        if (isWaitingForInput)
        {
            WaitForInput();
        }
    }

    private void StartFadeIn()
    {
        startedFadeIn = true;
        isWaitingForInput = false;
        startedWait = false;
        startedFadeOut = false;
        screenAlpha = 1;
        startTime = Time.time;
        currentTime = Time.time;
        deltaTime = currentTime - startTime;

    }

    private void HandleFadeIn()
    {
        percentToAdd = Time.deltaTime / fadeInDuration;
        screenAlpha -= percentToAdd;
        introImage.color = new Color(introImage.color.r, introImage.color.g, introImage.color.b, screenAlpha);

        //Quand on a fini le fade in on attend un peu si on veut
        if (deltaTime >= fadeInDuration)
        {
            StartWait();
        }
    }



    private void StartFadeOut()
    {
        startedFadeIn = false;
        isWaitingForInput = false;
        startedWait = false;
        startedFadeOut = true;
        startTime = Time.time;
        currentTime = Time.time;
        deltaTime = currentTime - startTime;


    }

    private void HandleFadeOut()
    {
        percentToAdd = Time.deltaTime / fadeOutDuration;
        screenAlpha += percentToAdd;
        introImage.color = new Color(introImage.color.r, introImage.color.g, introImage.color.b, screenAlpha);
        if (deltaTime >= fadeOutDuration)
        {
            startedFadeOut = false;
            screenAlpha = 1;
            introImage.color = new Color(introImage.color.r, introImage.color.g, introImage.color.b, screenAlpha);

            // Quand le fadeOut est fini on attend que le joeur appuie sur un bouton
            
            isWaitingForInput = true;
            startedFadeIn = false;
            startedWait = false;
           
        }
    }


    private void StartWait()
    {

        startedFadeIn = false;
        startedWait = true;
        isWaitingForInput = false;
        startedFadeOut = false;
        startTime = Time.time;
        currentTime = Time.time;
        deltaTime = currentTime - startTime;
        screenAlpha = 0;
        introImage.color = new Color(introImage.color.r, introImage.color.g, introImage.color.b, screenAlpha);


        // On change d'image avant de faire le fadeOut
        i++;
        if (i < introSprite.Length)
        {
            introImage.sprite = introSprite[i];
        }


      
    }

    private void WaitForInput()
    {
        // Si on appuie sur un bouton on fadeIn
        if (Input.anyKeyDown)
        {
            isWaitingForInput = false;

            StartFadeIn();
        }
    }

}
