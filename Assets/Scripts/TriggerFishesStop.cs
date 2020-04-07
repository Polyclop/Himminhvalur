using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFishesStop : MonoBehaviour
{
    public FollowTarget followScript;
    

    private void OnTriggerEnter(Collider other)
    {
        followScript.enabled = !followScript.enabled;
    }
}
