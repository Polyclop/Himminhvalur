using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doortrigger : MonoBehaviour
{
    [SerializeField] GameObject door;
    public bool isInZone = false;
    Animator zz_button;
    public Animator porte_point_pivot;
     void Start()
     {
        zz_button = GetComponent<Animator>();
        
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
            zz_button.SetBool("isPushed", true);
            porte_point_pivot.SetBool("isRotating", true);

        }

        if (zz_button.GetCurrentAnimatorStateInfo(0).normalizedTime >
           zz_button.GetCurrentAnimatorStateInfo(0).length && zz_button.GetCurrentAnimatorStateInfo(0).IsName("zz_ Bouton"))
        {
            
            zz_button.SetBool("isPushed", false);
        }
    }
}

