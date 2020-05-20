using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTriggers : MonoBehaviour
{
    public GameObject[] objects;


    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }
}
