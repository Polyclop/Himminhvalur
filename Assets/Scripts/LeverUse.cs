using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverUse : MonoBehaviour
{
    public bool isGrabbed = false;
    public bool isInside = false;
    Collider col;

    Animator anor;
    public Animator platfAnor;

    // Start is called before the first frame update
    void Start()
    {
        anor = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // si je suis dans la zone et que j'appuie sur "fire", j'active l'animation du levier
        if (isInside)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                anor.SetBool("leverActive", true);
            }
        }

        // si l'animation du levier est finie, je lance la deuxieme animation
        if(anor.GetCurrentAnimatorStateInfo(0).length >
            anor.GetCurrentAnimatorStateInfo(0).normalizedTime && anor.GetCurrentAnimatorStateInfo(0).IsName("leverActivate"))
        {
            
            platfAnor.SetBool("platfRemove", true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // si c'est bien le personnage je suis dans la zone
        if (other is CapsuleCollider)
        {
            isInside = true;
            col = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // si c'est bien le personnage je suis sorti de la zone
        if (other is CapsuleCollider)
        {
            isInside = false;
        }
    }
}
