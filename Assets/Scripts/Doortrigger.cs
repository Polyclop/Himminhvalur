using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doortrigger : MonoBehaviour
{
    [SerializeField] GameObject door;
    public bool isInZone = false;
    Animator button;
    public Animator porte;
    public Animator fenetre;

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
        
    }
    void OnTriggerEnter(Collider col)
    {
        isInZone = true;
    }
    void OnTriggerExit(Collider col)

    {
        isInZone = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isInZone)
        {
            button.SetBool("isPushed", true);

        }

    }

    // L'animation envoie un message quand elle est terminée
    public void AlertObserver(int message)
    {
        if(message == (int)animationState.ended)
        {
            porte.SetBool("isRotating", true);
            fenetre.SetBool("open", true);
            button.SetBool("isPushed", false);
        }
    }
}

