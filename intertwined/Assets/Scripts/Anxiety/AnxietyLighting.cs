using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class AnxietyLighting : MonoBehaviour
{
    public Light directionalLight;

    public Light boyLight;

    public Light dogLight;

    private bool crRunning = false;
    // Start is called before the first frame update
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
        if(!crRunning) StartCoroutine(DirectionalLightTransition(directionalLight, 1f, 0f));
        UnityEngine.RenderSettings.ambientLight = Color.black;
        boyLight.intensity = 8f-level;
        dogLight.intensity = 8f-level;
    }

    public void normalLighting()
    {
        if(!crRunning) StartCoroutine(DirectionalLightTransition(directionalLight, 1f, 1f));
        UnityEngine.RenderSettings.ambientLight = Color.gray;
        boyLight.intensity = 0f;
        dogLight.intensity = 0f;
    }
    
    IEnumerator DirectionalLightTransition(Light l, float duration, float targetIntensity)
    {
        crRunning = true;
        float startIntensity = l.intensity ;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            l.intensity = Mathf.Lerp(startIntensity, targetIntensity, currentTime / duration);
            yield return null;
        }
        crRunning = false;
        yield break;
    }
}
