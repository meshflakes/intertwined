using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyLighting : MonoBehaviour
{
    public Light directionalLight;

    public Light boyLight;

    public Light dogLight;
    // Start is called before the first frame update
    void Start()
    {
        directionalLight.intensity = 1f;
        boyLight.intensity = 0f;
        dogLight.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This function is for lighting effects when the characters are too far apart
    //Level can be 1-7, determining how far
    public void farLighting(float level)
    {
        directionalLight.intensity = 0f;
        boyLight.intensity = 8f-level;
        dogLight.intensity = 8f-level;
    }

    public void normalLighting()
    {
        directionalLight.intensity = 1f;
        boyLight.intensity = 0f;
        dogLight.intensity = 0f;
    }
}
