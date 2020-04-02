using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeFishes : MonoBehaviour
{
    public GameObject fish;

    public void AwakeFish()
    {
        fish.GetComponent<FollowTarget>().enabled = true;
        fish.GetComponent<Light>().enabled = true;
    }
}
