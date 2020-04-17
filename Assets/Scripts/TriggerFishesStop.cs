using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFishesStop : MonoBehaviour
{
    public FollowTarget followScript;
    public Rigidbody[] roomBody;
    AudioSource whaleSource;
    public bool comesFromLeft;
    public BoxCollider coll;
    public bool shallBlockPlayer;

    private void Start()
    {
        coll = GetComponent<BoxCollider>();
        whaleSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (shallBlockPlayer)
        {
            //shallBlockPlayer = false;
            coll.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other is CapsuleCollider)
            comesFromLeft = (other.transform.position.x < transform.position.x);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider)
        {
            if ((other.transform.position.x < transform.position.x) != comesFromLeft)
            {
                
                whaleSource.Play();
                shallBlockPlayer = true;
                for (int i = 0; i < roomBody.Length; i++)
                {
                    roomBody[i].isKinematic = false;
                    roomBody[i].useGravity = true;
                    
                    
                    roomBody[i].WakeUp();
                }
                followScript.enabled = !followScript.enabled;
            }
        }
    }
}
