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

    bool flipSprite;
    int rotator = 0;
    public float rotationZ;

    public bool isGrabbing;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        tsf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        GameEvents.current.onGrabObject += OnPlayerGrab;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        tsf.position = new Vector3(tsf.position.x + ((Input.GetAxis("Horizontal") * speed)), tsf.position.y, tsf.position.z);
        
        animator.SetBool("isWalking", Input.GetAxis("Horizontal") != 0);

        if(Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }

        if (!isGrabbing)
        {
            FlipCheck();
        }

    }

    void FlipCheck()
    {
        
        Vector2 scale = transform.localScale;
        if (rotator == 0 && Input.GetAxis("Horizontal") < 0 || rotator == 180 && Input.GetAxis("Horizontal") > 0) flipSprite = true;
        else flipSprite = false;
        if (flipSprite)
        {
            scale.x *= -1;
            
            if (rotator == 180)
            {
                rotator = 0;
                //maggieTransform.localPosition = new Vector2(transform.localPosition.x + ((maggieRenderer.size.x * maggieTransform.localScale.x) / 2), maggieTransform.localPosition.y);
            }
            else
            {
                rotator = 180;
                //maggieTransform.localPosition = new Vector2(transform.localPosition.x - ((maggieRenderer.size.x * maggieTransform.localScale.y) / 2), maggieTransform.localPosition.y);

            }
            
            
            //transform.localScale = scale;
            transform.Rotate(0, 0, 180, Space.Self);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrabbing)
            canJump = true;
    }

    private void OnPlayerGrab(Vector3 objectPosition)
    {
        isGrabbing = !isGrabbing;
        if ((Input.GetAxis("Horizontal") < 0 && transform.position.x < objectPosition.x) || (Input.GetAxis("Horizontal") > 0 && transform.position.x > objectPosition.x))
        {
            transform.Rotate(0, 0, 180, Space.Self);

        }
        canJump = !canJump;
        animator.SetBool("isPushing", isGrabbing);
    }
}
