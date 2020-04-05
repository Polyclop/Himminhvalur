using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whaleChangeSide : MonoBehaviour
{
    public whaleMove whaleMoveScript;
    public bool isInside;
    private void OnTriggerEnter(Collider other)
    {
        if(other is CapsuleCollider)
        {
            isInside = true;
            whaleMoveScript.moveRight = !whaleMoveScript.moveRight;
            whaleMoveScript.whalouTransform.position = transform.position;
            whaleMoveScript.pos = transform.position;
        }
    }
}
