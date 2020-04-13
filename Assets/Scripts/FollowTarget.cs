using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{


    public bool isInside;
    public Transform playerTransform;
    [Range(0, 5)]
    public float allowedDistanceClose = 0;
    [Range(0, 10)]
    public float allowedDistanceFar = 4;
    [Range(0, 5)]
    public float closeFollowSpeedWhenWalking = 0.9f;
    float closeFollowSpeedWhenRunning = 1.8f;
    float closeFollowSpeed= 0.9f;
    [Range(0, 5)]
    public float farFollowSpeedWhenWalking = 1.4f;
    float farFollowSpeedWhenRunning = 2.8f;
    float farFollowSpeed = 1.4f;

    float currentFollowSpeed = 0;
    Rigidbody rb;
    RaycastHit hit;
    SphereCollider fishCollider;

    int currentRoom;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fishCollider = GetComponent<SphereCollider>();
        //allowedDistance = playerTransform.gameObject.GetComponent<CapsuleCollider>().radius/2;
        GameEvents.current.onChangingRoom += onChangingFishesMovement;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(playerTransform);
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if(hit.distance >= allowedDistanceClose && hit.distance <= allowedDistanceFar)
            {
                currentFollowSpeed = closeFollowSpeed;
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, currentFollowSpeed * Time.deltaTime);
            }
            else if (hit.distance >= allowedDistanceFar)
            {
                currentFollowSpeed = farFollowSpeed;
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, currentFollowSpeed * Time.deltaTime);
            }
            else
            {
                currentFollowSpeed = 0;
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        isInside = true;
        if(other is CapsuleCollider)
        {
            GameEvents.current.BeSeen(isInside);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInside = false;
        if (other is CapsuleCollider)
        {
            GameEvents.current.BeSeen(isInside);
        }
    }

    private void onChangingFishesMovement(float room)
    {
        if(room >= 2)
        {
            fishCollider.enabled = false;
            closeFollowSpeed = closeFollowSpeedWhenRunning;
            farFollowSpeed = farFollowSpeedWhenRunning;
        }
        else
        {
            fishCollider.enabled = false;
            closeFollowSpeed = closeFollowSpeedWhenWalking;
            farFollowSpeed = farFollowSpeedWhenWalking;
        }
    }

}
