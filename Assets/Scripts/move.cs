using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Transform tsf;
    Rigidbody rb;
    Animator animator;
    public Transform childCenter;

    // Movement
    public bool canMove = true;
    [Range(0, 10)]
    public float walkSpeed = 1f;
    [Range(0, 10)]
    public float runSpeed = 1.6f;
    [Range(0, 10)]
    public float grabSpeed = 0.01f;
    float speed;

    // Jump
    public bool canJump = true;
    [Range(1, 20)]
    public float jumpVelocity;
    [Range(0, 10)]
    public float fallMultiplier = 2.5f;
    [Range(0, 10)]
    public float lowJumpMultiplier = 2f;
    
    // Changing Side
    bool flipSprite;
    int rotator = 0;
    public float rotationZ;

    // Others
    public bool isGrabbing;
    bool isRunning = false;

    //Timing for death wait
    bool deathTimerStarted;
    float startTime, currentTime, deltaTime;
    float waitDuration;


        /// Init
    void Start()
    {
        tsf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        speed = isRunning ? runSpeed : walkSpeed;
        GameEvents.current.onGrabObject += OnPlayerGrab;
        GameEvents.current.onBlockingPlayerMove += AllowPlayerMovement;
        GameEvents.current.onDying += Respawn;

    }

        /// Game Loop
    void Update()
    {


        HandleMove();
        HandleJump();
        if (!isGrabbing)
        {
            FlipCheck();
        }


        // Handle timer preventing to move when dead
        if (deathTimerStarted)
        {
            currentTime = Time.time;
            deltaTime = currentTime - startTime;
            if(deltaTime >= waitDuration)
            {
                canMove = true;
                canJump = true;
                deltaTime = 0;
                deathTimerStarted = false;
            }
        }
    }



        ///  MOVEMENT

    void HandleMove()
    {
        
        if (Input.GetButton("Jump")){
            isRunning = true;
            speed = isGrabbing ? grabSpeed : runSpeed;
        }
        else
        {
            isRunning = false;
            speed = isGrabbing ? grabSpeed : walkSpeed;
        }

        if (canMove)
        {
            tsf.position = new Vector3(tsf.localPosition.x + (Input.GetAxis("Horizontal") * speed * Time.deltaTime), tsf.localPosition.y, tsf.localPosition.z);
        }

        animator.SetBool("isWalking", Input.GetAxis("Horizontal") != 0 && canMove && !isRunning);
        animator.SetBool("isRunning", Input.GetAxis("Horizontal") != 0 && canMove && isRunning);

    }

    void HandleJump()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        /*
         * if (Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            rb.velocity = Vector3.up * jumpVelocity;
            animator.SetBool("isJumping", true);
            animator.SetBool("isWalking", false);
        }
        */
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
            tsf.Rotate(0, 180, 0, Space.World);
            childCenter.localPosition = new Vector3(childCenter.localPosition.x * -1, childCenter.localPosition.y, childCenter.localPosition.z);

        }

    }


        /// GAME EVENTS

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrabbing)
        {
            canJump = true;

            animator.SetBool("isJumping", false);

        }
            

    }

    private void OnPlayerGrab(Vector3 objectPosition)
    {
        isGrabbing = !isGrabbing;
        /*if ((Input.GetAxis("Horizontal") < 0 && transform.position.x < objectPosition.x) || (Input.GetAxis("Horizontal") > 0 && transform.position.x > objectPosition.x))
        {
            transform.Rotate(0, 180, 0, Space.Self);

        }*/
        canJump = !canJump;
        animator.SetBool("isPushing", isGrabbing);
        speed = isGrabbing ? grabSpeed : (isRunning ? runSpeed : walkSpeed); 
    }

    private void AllowPlayerMovement(bool isAllowed)
    {
        canMove = isAllowed;
    }
      
    /*
    private void ChangePlayerWalkType(float room)
    {
        // if player is in rooms 2 or 4 he's running
        isRunning = (room == 2 || room == 4);
        speed = isRunning ? runSpeed : walkSpeed;
    }
    */

    private void Respawn(float roomNumber, float respawnDuration)
    {
        // Set Position on respawn point
        transform.position = GameObject.FindGameObjectWithTag("Respawn " + roomNumber.ToString()).transform.position;
        // Cannot move for given duration
        startTime = currentTime = Time.time;
        deathTimerStarted = true;
        canMove = false;
        canJump = false;
        waitDuration = respawnDuration;
    }
}
