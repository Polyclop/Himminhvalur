using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBoxFall : MonoBehaviour
{
    public Rigidbody boxBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            boxBody.useGravity = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
