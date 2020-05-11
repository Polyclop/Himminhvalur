using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class AdaptFogHeight : MonoBehaviour
{
    
    VolumeProfile profile;
    Fog fog;
    Transform playerTransform;
    float deltaFogHeight;
    float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        profile = GetComponent<Volume>().profile;
        Fog og;
        if (profile.TryGet<Fog>(out og))
        {
            fog = og;
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        deltaFogHeight = playerTransform.position.y - fog.baseHeight.value;
        maxHeight = fog.maximumHeight.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.y - fog.baseHeight.value != deltaFogHeight)
        {
            fog.baseHeight.value = playerTransform.position.y - deltaFogHeight;
        }
        fog.maximumHeight.value = fog.baseHeight.value + maxHeight;
    }
  
}
