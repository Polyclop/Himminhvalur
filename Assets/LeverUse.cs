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
        if (isInside)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                anor.SetBool("leverActive", true);
            }
        }

        if(anor.GetCurrentAnimatorStateInfo(0).length >
            anor.GetCurrentAnimatorStateInfo(0).normalizedTime && anor.GetCurrentAnimatorStateInfo(0).IsName("leverActivate"))
        {
            
            platfAnor.SetBool("platfRemove", true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = true;
            col = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = false;
        }
    }
}
