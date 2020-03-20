using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightHitsTrigger : MonoBehaviour
{
    public bool isInside;
    public bool seen;
    private void Start()
    {
        GameEvents.current.onMovingInOutOfSafeSpace += OnHidingInSafeSpace;
    }
    private void OnTriggerEnter(Collider other)
    {
        isInside = true;

        //if(other is CapsuleCollider)
        GameEvents.current.BeSeen(isInside & seen);
          
    }

    private void OnTriggerStay(Collider other)
    {
        GameEvents.current.BeSeen(isInside & seen);
    }

    private void OnTriggerExit(Collider other)
    {
        isInside = false;
        //if (other is CapsuleCollider)
        GameEvents.current.BeSeen(isInside);
    }


    void OnHidingInSafeSpace(bool safe)
    {
        seen = !safe;
    }


}
