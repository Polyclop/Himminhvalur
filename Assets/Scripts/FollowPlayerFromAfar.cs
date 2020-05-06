using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerFromAfar : MonoBehaviour
{
    
    public float activeInRoom;
    bool isFollowing, shallMoveToDeathPoint;
    Transform playerTransform;
    [Range(0, 10)]
    public float WhaleMoveSpeed;
    public Transform deathTransform;
    Vector3 initialPosition;

    //Death Delegate
    public delegate void OnDeathDelegate();
    public static OnDeathDelegate deathDelegate;

    public void OnDeath()
    {
        deathDelegate();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onChangingRoom += PrepareToFollow;
        GameEvents.current.onDying += InitBackPosition;
        caughtJauge.deathDelegate += PrepareForDeath;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        }
        if (shallMoveToDeathPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, deathTransform.position, WhaleMoveSpeed * Time.deltaTime);
            if (transform.position == deathTransform.position)
            {
                shallMoveToDeathPoint = false;
                OnDeath();
            }
        }
    }

    private void PrepareToFollow(float room)
    {
        isFollowing = (room == activeInRoom);
    }

    private void PrepareForDeath()
    {
        shallMoveToDeathPoint = true;
        isFollowing = false;
        initialPosition = transform.position;
    }

    private void InitBackPosition(float room, float deathDuration)
    {
        shallMoveToDeathPoint = false;
        transform.position = initialPosition;
        isFollowing = true;
    }

}
