using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightHitsTrigger : MonoBehaviour
{
    public bool isInside;
    private void OnTriggerEnter(Collider other)
    {
        isInside = true;
        //if(other is CapsuleCollider)
            GameEvents.current.BeSeen(isInside);
    }

    private void OnTriggerExit(Collider other)
    {
        isInside = false;
        //if (other is CapsuleCollider)
        GameEvents.current.BeSeen(isInside);
    }


}
