using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerPointRoom2 : MonoBehaviour
{

    public bool comesFromLeft;
    public Animator animator;
    public Rigidbody[] rb;
    CinemachineImpulseSource impulse;

    int rand;
    Vector3 impulseDirection;

    public bool enableTestImpulse;

    enum cameraDirection
    {
        forward = 0,
        backward = 1,
        up = 2,
        down = 3,
        left = 4,
        right = 5
    }

    cameraDirection randomDirection;

    // Start is called before the first frame update
    void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(enableTestImpulse && Input.GetKeyDown(KeyCode.Return))
        {
            ImpulseAtRandomDirection();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other is CapsuleCollider)
        {
            TriggerGodsWrath();
            // Check where the player comes from
            comesFromLeft = (other.transform.position.x < transform.position.x);
            ImpulseAtRandomDirection();
            
        }
    }

    private void ImpulseAtRandomDirection()
    {
        impulse.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = Random.Range(0.2f, 0.9f);
        impulse.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = Random.Range(0, 0.3f);
        randomDirection = (cameraDirection)Random.Range(2, 6); ;
        switch (randomDirection)
        {
            case cameraDirection.forward :
                impulseDirection = Camera.main.transform.forward;
                break;
            case cameraDirection.backward:
                impulseDirection = -Camera.main.transform.forward;
                break;
            case cameraDirection.up:
                impulseDirection = Camera.main.transform.up;
                break;
            case cameraDirection.down:
                impulseDirection = -Camera.main.transform.up;
                break;
            case cameraDirection.left:
                impulseDirection = -Camera.main.transform.right;
                break;
            case cameraDirection.right:
                impulseDirection = Camera.main.transform.right;
                break;
        }
        impulse.GenerateImpulse(impulseDirection);
    }

    private void OnTriggerExit(Collider other)
    {
        /*
        // Check if the player exits from the oposite direction
        if (other is CapsuleCollider)
        {

            if ((other.transform.position.x < transform.position.x) != comesFromLeft)
            {
                TriggerGodsWrath();
            }

        }
        */
    }


    private void TriggerGodsWrath()
    {
        if (animator != null)
            animator.SetTrigger("trigger");
        for (int i=0; i< rb.Length; i++)
        {
            rb[i].isKinematic = false;
            rb[i].useGravity = true;
            //boxBody[i].useGravity = true;
            rb[i].WakeUp();

        }

    }
}
