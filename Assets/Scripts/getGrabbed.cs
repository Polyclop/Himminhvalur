using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getGrabbed : MonoBehaviour
{
    public bool isGrabbed = false;
    public bool isInside = false;
    Collider col;
    public Transform playerTransform;

    AudioSource audSource;
    bool didPlaySource;
    float baseValue;
    [Range(0, 1)]
    public float maxVolume = 0.5f;
    [Range(0, 1f)]
    public float growthValue = 0.1f;
    [Range(0, 1f)]
    public float decreaseValue = 0.2f;

    Material outlineMaterial;

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
        audSource = GetComponent<AudioSource>();
        outlineMaterial = GetComponent<Renderer>().materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isGrabbed && playerTransform.rotation.y > 0)
                {
                    isGrabbed = true;
                    Grab();
                    GameEvents.current.GrabObject(transform.position);
                }
                
                /*
                 * isGrabbed = !isGrabbed;
                if (!isGrabbed)
                    Ungrab();

                GameEvents.current.GrabObject(transform.position);
                if (isGrabbed)
                {
                    Grab();
                    
                }
                */
            }
            else
            {
                if (isGrabbed)
                {
                    GameEvents.current.GrabObject(transform.position);
                    isGrabbed = false;
                    Ungrab();

                }
            }
        }

        if (isGrabbed && Input.GetAxis("Horizontal") != 0)
        {
            if (!didPlaySource)
            {
                didPlaySource = true;
                audSource.Play();
            }
            if (audSource.volume >= maxVolume)
                audSource.volume = maxVolume;
            else
                audSource.volume += growthValue * Time.deltaTime;
        }
        else
        {
            if (audSource.volume <= baseValue)
            {
                audSource.volume = baseValue;
                didPlaySource = false;
                audSource.Stop();
            }
            else
                audSource.volume -= decreaseValue * Time.deltaTime;
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
            outlineMaterial.SetFloat("Boolean_5842AB85", 1);
            //Physics.IgnoreCollision(other, boxCollider, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = false;
            outlineMaterial.SetFloat("Boolean_5842AB85", 0);
            //Physics.IgnoreCollision(other, boxCollider, false);
        }
    }



    void Grab()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
            playerTransform.gameObject.GetComponent<move>().Flip();
        }
        this.transform.parent = col.transform;
        //shallTriggerLights = true;
        //lightHits.enabled = true;
    }

    void Ungrab()
    {
        this.transform.parent = null;
    }


}
