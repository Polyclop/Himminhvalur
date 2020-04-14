using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFishesStop : MonoBehaviour
{
    public FollowTarget followScript;
    public Rigidbody[] roomBody;
    AudioSource whaleSource;

    private void OnTriggerExit(Collider other)
    {
        followScript.enabled = !followScript.enabled;
        for(int i=0; i< roomBody.Length; i++)
        {
            roomBody[i].useGravity = !roomBody[i].useGravity;
            roomBody[i].isKinematic = !roomBody[i].isKinematic;
        }
        whaleSource.Play();
    }
}
