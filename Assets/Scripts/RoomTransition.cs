using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class RoomTransition : MonoBehaviour
{

    public CinemachineVirtualCamera[] cameras;
    public bool isVertical;
    bool comesFromLeft;

    // Room Number
    public float leftRoomNumber;
    public float rightRoomNumber;

    public float currentRoom;

    AudioSource audSource;
    bool didPlayAudio;

    public bool isLastRoom;
    public float blendTimeForLastRoom;
    CinemachineBrain cameraBrain;

    VolumeProfile profile;
    Fog fog;
    AdaptFogHeight adaptFogHeightScript;
    [Range(0, 100)]
    public float wantedFogAttenuationDistance = 10;
    [Range(0, 100)]
    public float wantedFogBaseHeight = -25;
    [Range(0, 100)]
    public float wantedFogMaxHeight = 50;
    float ogFogAttenuationDistance, ogFogBaseHeight, ogFogMaxHeight;

    bool startedBlendForFog;
    float startTime, currentTime, percentToAdd;





        // Start is called before the first frame update
    void Start()
    {
        currentRoom = leftRoomNumber;
        audSource = GetComponent<AudioSource>();
        if (isLastRoom)
        {
            cameraBrain = Camera.main.GetComponent<CinemachineBrain>();
            adaptFogHeightScript = Camera.main.GetComponent<AdaptFogHeight>();
            profile = Camera.main.GetComponent<Volume>().profile;
            Fog og;
            if (profile.TryGet<Fog>(out og))
            {
                fog = og;
            }
        }
    }


    private void Update()
    {
        if (startedBlendForFog)
        {
            currentTime = Time.time;

            percentToAdd = Time.deltaTime / blendTimeForLastRoom;

            
            fog.baseHeight.value += percentToAdd * (wantedFogBaseHeight - ogFogBaseHeight);
            fog.maximumHeight.value += percentToAdd * (wantedFogMaxHeight - ogFogMaxHeight);
            fog.meanFreePath.value += percentToAdd * (wantedFogAttenuationDistance - ogFogAttenuationDistance);

            if (currentTime - startTime >= blendTimeForLastRoom)
            {
                startedBlendForFog = false;
                fog.baseHeight.value = wantedFogBaseHeight;
                fog.maximumHeight.value = wantedFogMaxHeight;
                fog.meanFreePath.value = wantedFogAttenuationDistance;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other is CapsuleCollider)
        {
            // Check where the player comes from
            if (isVertical)
            {
                comesFromLeft = (other.transform.position.x < transform.position.x);
                currentRoom = comesFromLeft ? leftRoomNumber : rightRoomNumber;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits from the oposite direction
        if(other is CapsuleCollider)
        {
            if (isVertical)
            {
                if((other.transform.position.x < transform.position.x) != comesFromLeft)
                {
                    ChangeCamera();
                }
            }
            else
            {
                ChangeCamera();
            }
        }
    }

    void ChangeCamera()
    {
        if (isLastRoom)
        {
            cameraBrain.m_DefaultBlend.m_Time = blendTimeForLastRoom;
            GetComponent<BoxCollider>().isTrigger = false;
            adaptFogHeightScript.enabled = false;
            startedBlendForFog = true;
            startTime = Time.time;
            ogFogAttenuationDistance = fog.meanFreePath.value;
            ogFogBaseHeight = fog.baseHeight.value;
            ogFogMaxHeight = fog.maximumHeight.value;
        }
        // active Camera's priority is 11,
        // others are 10
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].Priority == 10)
            {
                cameras[i].Priority = 11;
            }
            else cameras[i].Priority = 10;
        }

        // Room Change Event
        currentRoom = currentRoom == leftRoomNumber ? rightRoomNumber : leftRoomNumber;
        GameEvents.current.ChangeRoom(currentRoom);

        if(audSource != null && !didPlayAudio)
        {
            didPlayAudio = true;
            audSource.Play();
        }


    }
}
