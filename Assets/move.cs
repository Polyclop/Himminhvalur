using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Transform tsf;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        tsf = GetComponent<Transform>();    
    }

    // Update is called once per frame
    void Update()
    {
        tsf.position = new Vector3(tsf.position.x + ((Input.GetAxis("Horizontal") * speed)), tsf.position.y, tsf.position.z);
    }
}
