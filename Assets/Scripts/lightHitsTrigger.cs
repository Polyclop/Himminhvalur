using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightHitsTrigger : MonoBehaviour
{
    public bool isInside;
    public bool seen = true;
    private void Start()
    {
        GameEvents.current.onMovingInOutOfSafeSpace += OnHidingInSafeSpace;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = true;
            GameEvents.current.BeSeen(isInside & seen);
        }
            
          
    }

    private void OnTriggerStay(Collider other)
    {
        if (other is CapsuleCollider)
        {
            GameEvents.current.BeSeen(isInside & seen);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = false;
            GameEvents.current.BeSeen(isInside);
        }
    }


    void OnHidingInSafeSpace(bool safe)
    {
        seen = !safe;
    }


}
