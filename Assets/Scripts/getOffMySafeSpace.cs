using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getOffMySafeSpace : MonoBehaviour
{
    public bool isInside = false;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = true;
            col = other;
            GameEvents.current.MoveInOutOfSafeSpace(isInside);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = false;
            GameEvents.current.MoveInOutOfSafeSpace(isInside);
        }
    }

}
