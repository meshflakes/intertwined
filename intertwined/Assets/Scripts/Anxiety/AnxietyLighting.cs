using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class AnxietyLighting : MonoBehaviour
{
    public Light directionalLight;

    public Light boyLight;

    public Light dogLight;

    private Color colorStart = Color.gray;
    private Color colorEnd = Color.gray;

    private bool dlCrRunning = false;
    private bool alCrRunning = false;
    
    void Start()
    {
        directionalLight.intensity = 1f;
        boyLight.intensity = 0f;
        dogLight.intensity = 0f;
    }

    
    //This function is for lighting effects when the characters are too far apart
    //Level can be 1-7, determining how far
    public void farLighting(float level)
    {
        if(!dlCrRunning) StartCoroutine(DirectionalLightTransition(directionalLight, 1f, 0f));
        //UnityEngine.RenderSettings.ambientLight = Color.black;
        if (!alCrRunning) StartCoroutine(ColorTransition(UnityEngine.RenderSettings.ambientLight, Color.black, 1f));
        boyLight.intensity = 8f-level;
        dogLight.intensity = 8f-level;
    }

    public void normalLighting(float dlIntensity)
    {
        if(!dlCrRunning) StartCoroutine(DirectionalLightTransition(directionalLight, 1f, dlIntensity));
        //UnityEngine.RenderSettings.ambientLight = Color.gray;
        if (!alCrRunning) StartCoroutine(ColorTransition(UnityEngine.RenderSettings.ambientLight, Color.gray, 1f));
        colorEnd = Color.gray;
        boyLight.intensity = 0f;
        dogLight.intensity = 0f;
    }
    
    IEnumerator DirectionalLightTransition(Light l, float duration, float targetIntensity)
    {
        dlCrRunning = true;
        float startIntensity = l.intensity ;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            l.intensity = Mathf.Lerp(startIntensity, targetIntensity, currentTime / duration);
            yield return null;
        }
        dlCrRunning = false;
        yield break;
    }

    IEnumerator ColorTransition(Color startColor, Color endColor, float duration)
    {
        alCrRunning = true;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            UnityEngine.RenderSettings.ambientLight = Color.Lerp(startColor, endColor, currentTime / duration);
            yield return null;
        }

        alCrRunning = false;
        yield break;
    }
}
