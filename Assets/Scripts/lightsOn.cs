using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightsOn : MonoBehaviour
{
    public Material mat;
    public Color currentColor;
    float colorMultiplier = 0;
    public float intensityScale = 0.1f;

    public bool goingUp = true;

    public float maxIntensity = 5;

    // Start is called before the first frame update
    void Start()
    {
        currentColor = mat.GetColor("_EmissionColor");
        mat.SetColor("_EmissionColor", currentColor*5); 
    }

    // Update is called once per frame
    void Update()
    {
        if (goingUp)
        {           
            colorMultiplier += intensityScale;
            if(colorMultiplier >= maxIntensity)
            {
                goingUp = !goingUp;
            }
        }
        else
        {
            colorMultiplier -= intensityScale;
            if (colorMultiplier <= 0)
            {
                goingUp = !goingUp;
            }
        }
        mat.SetColor("_EmissionColor", currentColor * colorMultiplier);
    }
}
