using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomTransition : MonoBehaviour
{

    public CinemachineVirtualCamera[] cameras;
    Vector3[] camerasPosition;
    public bool isVertical;
    bool comesFromLeft;
    bool shallPrepareToChangeCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] camerasPosition = new Vector3[cameras.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (shallPrepareToChangeCamera)
        {
            /*
            for (int i = 0; i < cameras.Length; i++)
            {
                camerasPosition[i] = cameras[i].transform.position - cameras[i].body;
            }

            // When cameras are at the same place we switch them
            if (camerasPosition[0].x == camerasPosition[1].x && camerasPosition[0].y == camerasPosition[1].y)
            {
            */
                ChangeCamera();
                shallPrepareToChangeCamera = false;
            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other is CapsuleCollider)
        {
            // Check where the player comes from
            if (isVertical)
            {
                comesFromLeft = (other.transform.position.x < transform.position.x);
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits from the oposite direction
        if(other is CapsuleCollider)
        {
            if (isVertical)
            {
                if((other.transform.position.x < transform.position.x) != comesFromLeft)
                {

                    shallPrepareToChangeCamera = true;
                }
                
            }
            else
            {
                shallPrepareToChangeCamera = true;
            }
        }
    }

    void ChangeCamera()
    {
        // active Camera's priority is 11,
        // others are 10
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].Priority == 10)
            {
                cameras[i].Priority = 11;
            }
            else cameras[i].Priority = 10;
        }
    }
}
