using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleMove : MonoBehaviour
{
    [Range(0, 100)]
    public float magnitude = 1;
    [Range(0, 10)]
    public float frequency = 1;
    public bool moveRight;
    public Vector3 pos;
    [Range(0, 100)]
    public float moveSpeed;
    Vector3 nextPosition;
    Vector3 direction;

    [Range(0, 100)]
    public float rotationForce;

    bool movedRight;

    [HideInInspector]
    public Transform whalouTransform;

    float angle;


    // Whale pattern depends on room
    float currentRoom;
    public bool shallMove;
    Animator whaleAnimator;
    Transform transformToFollow;
    public Transform eyeTransform;
    public GameObject whaleSpawn3;

    [Range(0, 20)] public float whaleOffset;


    Material whaleMaterial;
    [ColorUsage(true, true)]
    public Color whaleBaseEmissionColor;
    [ColorUsage(true, true)]
    public Color whaleHighIntensityColor;

    // Start is called before the first frame update
    void Start()
    {
        whalouTransform = GetComponent<Transform>();
        whaleAnimator = GetComponent<Animator>();
        pos = whalouTransform.position;
        GameEvents.current.onChangingRoom += AdaptWhalePattern;
        whaleSpawn3 = GameObject.FindGameObjectWithTag("WhaleSpawn3");
        transformToFollow = GameObject.FindGameObjectWithTag("WhaleSpawn1.5").transform;

        whaleMaterial = GetComponentInChildren<Renderer>().material;
        whaleBaseEmissionColor = whaleMaterial.GetColor("Color_FD08B2B8");
        whaleHighIntensityColor = new Color(whaleBaseEmissionColor.r * 500, whaleBaseEmissionColor.g * 500, whaleBaseEmissionColor.b * 500);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //nextPosition = Move();
        //transform.LookAt(nextPosition);
        //direction = nextPosition - transform.position;
        //angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    void Move()
    {

        if (shallMove)
        {
            
            if (moveRight)
            {
                pos += transform.right * moveSpeed * Time.deltaTime;
                whalouTransform.position = pos + transform.up * (Mathf.Sin(-Time.time * frequency) + whaleOffset) * magnitude;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin((Time.time * frequency) - 1 + whaleOffset) * magnitude * rotationForce);


            }
            else
            {
                pos -= transform.right * moveSpeed * Time.deltaTime;
                whalouTransform.position = pos + transform.up * (Mathf.Sin(Time.time * frequency) + whaleOffset) * magnitude;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin((Time.time * frequency) - 1 + whaleOffset) * magnitude * rotationForce);


            }
            if (movedRight != moveRight)
            {
                whalouTransform.localScale *= -1;
            }
            movedRight = moveRight;
        }
        else
        {
            transform.position = transformToFollow.position - eyeTransform.position + transform.position;
        }

        
            
    }

    void AdaptWhalePattern(float room)
    {
        currentRoom = room;
        switch (currentRoom)
        {
            case 1.5f:
                shallMove = false;
                transformToFollow = GameObject.FindGameObjectWithTag("WhaleSpawn1.5").transform;
                transform.localRotation = Quaternion.Euler(0, -90, -20);
                whalouTransform.localScale *= moveRight ? -1 : 1;
                moveRight = movedRight = false;
                whaleMaterial.SetColor("Color_FD08B2B8", whaleBaseEmissionColor);
                break;
            case 2: shallMove = false;
                transformToFollow = GameObject.FindGameObjectWithTag("WhaleSpawn2").transform;
                transform.localRotation = Quaternion.Euler(0, -90, -20);
                whalouTransform.localScale *= moveRight ? -1 : 1;
                moveRight = movedRight = false;
                whaleMaterial.SetColor("Color_FD08B2B8", whaleBaseEmissionColor);

                break;
            case 3: shallMove = true;
                transform.position = whaleSpawn3.transform.position;
                transform.localRotation = Quaternion.Euler(0, -90, 0);
                pos = whalouTransform.position;
                whaleMaterial.SetColor("Color_FD08B2B8", whaleHighIntensityColor);
                break;
            case 3.5f: shallMove = true;
                transform.position = whaleSpawn3.transform.position;
                transform.localRotation = Quaternion.Euler(0, -90, 0);
                pos = whalouTransform.position;
                whaleMaterial.SetColor("Color_FD08B2B8", whaleHighIntensityColor);
                break;
            case 4:
                shallMove = false;
                transformToFollow = GameObject.FindGameObjectWithTag("WhaleSpawn4").transform;
                transform.localRotation = Quaternion.Euler(0, -90, -20);
                whalouTransform.localScale *= moveRight ? -1 : 1;
                moveRight = movedRight = false;
                whaleMaterial.SetColor("Color_FD08B2B8", whaleBaseEmissionColor);
                break;
            case 5:
                shallMove = false;
                transformToFollow = GameObject.FindGameObjectWithTag("WhaleSpawn5").transform;
                transform.localRotation = Quaternion.Euler(0, -90, -20);
                whalouTransform.localScale *= moveRight ? 1 : -1;
                moveRight = movedRight = true;
                whaleMaterial.SetColor("Color_FD08B2B8", whaleBaseEmissionColor);
                break;
            default: shallMove = false;
                break;
        }
        whaleAnimator.SetBool("isMoving", shallMove);
    }
}
