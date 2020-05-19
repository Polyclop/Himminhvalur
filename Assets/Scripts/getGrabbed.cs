using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

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

    move moveScript;
    bool startedWaitingForGrab;
    float startTime, currentTime;

    public Transform dedicatedSpawnPoint;

    int playerID = 0;
    Player player;


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
        player = ReInput.players.GetPlayer(playerID);

        audSource = GetComponent<AudioSource>();
        outlineMaterial = GetComponent<Renderer>().materials[1];
        moveScript = playerTransform.gameObject.GetComponent<move>();
        GameEvents.current.onDying += MoveBackObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside)
        {
            if (player.GetButton("Interact"))
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

        }
        if (isGrabbed && !player.GetButton("Interact"))
        {
            GameEvents.current.GrabObject(transform.position);
            isGrabbed = false;
            Ungrab();

        }

        if (isGrabbed && player.GetAxis("Move") != 0)
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

        if (startedWaitingForGrab)
        {
            currentTime = Time.time;
            if(currentTime - startTime >= 0.2f){
                startedWaitingForGrab = false;
                this.transform.parent = col.transform;
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
        
        if (player.GetAxis("Move") < 0)
        {
            moveScript.flipSprite = true;
            moveScript.Flip();
        }
        if (!startedWaitingForGrab)
        {
            startedWaitingForGrab = true;
            startTime = Time.time;
        }

        
        //shallTriggerLights = true;
        //lightHits.enabled = true;
    }

    void Ungrab()
    {
        this.transform.parent = null;
    }

    private void MoveBackObject(float room, float initDuration)
    {
        if(room == 4 && dedicatedSpawnPoint != null)
        {
            transform.position = dedicatedSpawnPoint.position;
        }
    }
}
