using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightHitsRaycast : MonoBehaviour
{
    public RaycastHit hit;
    Vector3 direction; // vector between light and player;
    public Transform playerTransform;
    public caughtJauge jauge;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = playerTransform.position - transform.position;
        if(Physics.Linecast(transform.position, playerTransform.position, out hit))
        //if(Physics.Raycast(transform.position, direction, out hit))
        {
            Debug.Log(hit);
            jauge.seen = hit.transform.GetComponent<CapsuleCollider>() != null;
        }    
    }
}
