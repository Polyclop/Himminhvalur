using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Doortrigger : MonoBehaviour
{
    public bool isInZone = false;
    Animator button;
    public Animator porte;
    
    AudioSource levierAudio;
   
    public AudioSource doorAudio;
    bool didOpenDoor;

    Material outlineMaterial, levierBaseOutlineMaterial;
    public MeshRenderer levierBaseRenderer;

    int playerID = 0;
    Player player;

    // etat de l'animation
    enum animationState
    {
        started = 0,
        inPause = 1,
        ended = 2
    };

    void Start()
    {
        button = GetComponent<Animator>();
        levierAudio = GetComponent<AudioSource>();

        outlineMaterial = GetComponent<Renderer>().materials[1];
        levierBaseOutlineMaterial = levierBaseRenderer.materials[1];

        player = ReInput.players.GetPlayer(playerID);

    }

    void OnTriggerEnter(Collider col)
    {
        isInZone = true;
        outlineMaterial.SetFloat("Boolean_5842AB85", 1);
        levierBaseOutlineMaterial.SetFloat("Boolean_5842AB85", 1);
    }

    void OnTriggerExit(Collider col)

    {
        isInZone = false;
        outlineMaterial.SetFloat("Boolean_5842AB85", 0);
        levierBaseOutlineMaterial.SetFloat("Boolean_5842AB85", 0);

    }

    void Update()
    {
        if (player.GetButtonDown("Interact") && isInZone && !didOpenDoor && !button.GetBool("activate"))
        {
            didOpenDoor = true;
            
            GameEvents.current.BlockPlayerMove(false);
            button.SetBool("activate", true);
            levierAudio.Play();

            
        }

    }

    // L'animation envoie un message quand elle est terminée
    public void AlertObserver(int message)
    {
        if(message == (int)animationState.ended)
        {
            porte.SetBool("isRotating", true);
            if (doorAudio != null)
            {
                doorAudio.Play();
            }
            GameEvents.current.BlockPlayerMove(true);
            didOpenDoor = false;
        }
    }
}

