﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getGrabbed : MonoBehaviour
{
    public bool isGrabbed = false;
    public bool isInside = false;
    Collider col;
    
    //triggerLights
    /*bool shallTriggerLights;
    public Light lit;
    float maxIntensity;
    public float intensityIncrease = 0.001f;
    public lightHitsRaycast lightHits;*/

    //Collider[] colliders;
    //Collider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        //maxIntensity = lit.intensity;
        //lit.intensity = 0;
        /*colliders = GetComponents<BoxCollider>();
        foreach (Collider coll in colliders)
        {
            if (!coll.isTrigger)
            {
                boxCollider = coll;
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                
                isGrabbed = !isGrabbed;
                if (!isGrabbed)
                    Ungrab();

                GameEvents.current.GrabObject(transform.position);
                if (isGrabbed)
                {
                    Grab();
                }


            }
        }

        



        /*
        if (shallTriggerLights && lit.intensity < maxIntensity)
        {
            lit.intensity += intensityIncrease;
        }
        */
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = true;
            col = other;
            //Physics.IgnoreCollision(other, boxCollider, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
            if (other is CapsuleCollider)
            {
                isInside = false;
                //Physics.IgnoreCollision(other, boxCollider, false);
            }
    }



    void Grab()
    {
        this.transform.parent = col.transform;
        //shallTriggerLights = true;
        //lightHits.enabled = true;
    }

    void Ungrab()
    {
        this.transform.parent = null;
    }


}
