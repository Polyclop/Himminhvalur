using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomTransition : MonoBehaviour
{

    public CinemachineVirtualCamera[] cameras;
    public bool isVertical;
    bool comesFromLeft;

    // Room Number
    public float leftRoomNumber;
    public float rightRoomNumber;

    public float currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        currentRoom = leftRoomNumber;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other is CapsuleCollider)
        {
            // Check where the player comes from
            if (isVertical)
            {
                comesFromLeft = (other.transform.position.x < transform.position.x);
                currentRoom = comesFromLeft ? leftRoomNumber : rightRoomNumber;
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
                    ChangeCamera();
                }
            }
            else
            {
                ChangeCamera();
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

        // Room Change Event
        currentRoom = currentRoom == leftRoomNumber ? rightRoomNumber : leftRoomNumber;
        GameEvents.current.ChangeRoom(currentRoom);
    }
}
