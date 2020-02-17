using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getGrabbed : MonoBehaviour
{
    public bool isGrabbed = false;
    public bool isInside = false;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isGrabbed = !isGrabbed;
                if (isGrabbed)
                {
                    Grab();
                }
                else Ungrab();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            isInside = true;
            col = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
            if (other is CapsuleCollider)
            {
                isInside = false;
            }
    }



    void Grab()
    {
        this.transform.parent = col.transform;
    }

    void Ungrab()
    {
        this.transform.parent = null;
    }


}
