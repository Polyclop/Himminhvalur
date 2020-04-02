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

    // Le nom de mon event 
    // et le type de paramètre que je vais transmettre quand je le déclencherai
    public event Action<Vector3> onGrabObject;

    public event Action<bool> onBeingSeen;

    public event Action<bool> onMovingInOutOfSafeSpace;

    // La Fonction liée a cet event qui va s'activer lors de l'activation
    public void GrabObject(Vector3 objectPosition)
    {
        if(onGrabObject != null)
        {
            onGrabObject(objectPosition);
        }
    }

    public void BeSeen(bool seen)
    {
        if (onBeingSeen != null)
        {
            onBeingSeen(seen);
        }
    }


    public void MoveInOutOfSafeSpace(bool safe)
    {
        if (onMovingInOutOfSafeSpace != null)
        {
            onMovingInOutOfSafeSpace(safe);
        }
    }
}
