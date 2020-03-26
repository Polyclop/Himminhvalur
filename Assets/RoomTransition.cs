using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomTransition : MonoBehaviour
{

    public GameObject[] cameras;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other is CapsuleCollider)
        {
            for(int i = 0; i< cameras.Length; i++)
            {
                cameras[i].SetActive(!cameras[i].activeSelf);
            }
        }
    }
}
