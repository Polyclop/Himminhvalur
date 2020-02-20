using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Transform tsf;
    public float speed = 1;
    public float jumpHeight = 20;
    public bool canJump = true;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        tsf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tsf.position = new Vector3(tsf.position.x + ((Input.GetAxis("Horizontal") * speed)), tsf.position.y, tsf.position.z);

        if(Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }

}
