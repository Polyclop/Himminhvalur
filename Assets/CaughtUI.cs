using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtUI : MonoBehaviour
{
    SpriteRenderer imageRenderer;
    public bool seen;

    // Start is called before the first frame update
    void Start()
    {
        imageRenderer = GetComponent<SpriteRenderer>();
        imageRenderer.enabled = false;
        GameEvents.current.onBeingSeen += CheckWhenSeen;
    }

    private void CheckWhenSeen(bool caught)
    {
        imageRenderer.enabled = caught;
    }
}
