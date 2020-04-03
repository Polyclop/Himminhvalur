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
    Vector3 pos;
    [Range(0, 100)]
    public float moveSpeed;
    Vector3 nextPosition;
    Vector3 direction;

    [Range(0, 100)]
    public float rotationForce;


    float angle;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Move();
        //nextPosition = Move();
        //transform.LookAt(nextPosition);
        //direction = nextPosition - transform.position;
        //angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin((Time.time * frequency) - 1) * magnitude * rotationForce);
    }

    Vector3 Move()
    {
        Vector3 newPosition;

        if (moveRight)
        {
            pos += transform.right * moveSpeed * Time.deltaTime;
        }
        else
        {
            pos -= transform.right * moveSpeed * Time.deltaTime;
        }

        newPosition = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        return newPosition;
        
            
    }
}
