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
    public float closeFollowSpeed = 0.9f;
    [Range(0, 5)]
    public float farFollowSpeed = 1;
    float currentFollowSpeed = 0;
    Rigidbody rb;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //allowedDistance = playerTransform.gameObject.GetComponent<CapsuleCollider>().radius/2;
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

}
