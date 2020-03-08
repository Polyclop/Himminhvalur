using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<Vector3> onGrabObject;

    public void GrabObject(Vector3 objectPosition)
    {
        if(onGrabObject != null)
        {
            onGrabObject(objectPosition);
        }
    }

}
