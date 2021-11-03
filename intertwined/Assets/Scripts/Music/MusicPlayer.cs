using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource ASL1;
    public AudioSource ASL2;
    public AudioSource ASL3;
    public AudioSource ASL4;
    public AudioSource ASL5;
    public AudioSource ParkAmbience1;
    public AudioSource ParkAmbience2;
    public AudioSource ParkAmbience3;
    
    private Dictionary<int, int[]> parkLevelMappings = new Dictionary<int, int[]>
    {
        {1, new int[] {1, 1, 0, 0, 0, 1, 1, 0}},
        {2, new int[] {1, 1, 1, 0, 0, 1, 1, 0}},
        {3, new int[] {1, 1, 1, 1, 0, 1, 1, 0}},
        {4, new int[] {1, 1, 1, 1, 1, 1, 1, 0}}
    };

    private static float TRANSITION_DURATION = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        ASL1.Play();
        ASL2.Play();
        ASL3.Play();
        ASL4.Play();
        ASL5.Play();
        ParkAmbience1.Play();
        ParkAmbience2.Play();
        ParkAmbience3.Play();
        ASL1.volume = 1;
        ParkAmbience1.volume = 1;
    }
    
    public void playParkLevelTracks(int level)
    {
        int[] volumes;
        parkLevelMappings.TryGetValue(level, out volumes);
        StartCoroutine(AudioTransition(ASL1, TRANSITION_DURATION, volumes[0]));
        StartCoroutine(AudioTransition(ASL2, TRANSITION_DURATION, volumes[1]));
        StartCoroutine(AudioTransition(ASL3, TRANSITION_DURATION, volumes[2]));
        StartCoroutine(AudioTransition(ASL4, TRANSITION_DURATION, volumes[3]));
        StartCoroutine(AudioTransition(ASL5, TRANSITION_DURATION, volumes[4]));
        StartCoroutine(AudioTransition(ParkAmbience1, TRANSITION_DURATION, volumes[5]));
        StartCoroutine(AudioTransition(ParkAmbience2, TRANSITION_DURATION, volumes[6]));
        StartCoroutine(AudioTransition(ParkAmbience3, TRANSITION_DURATION, volumes[7]));
    }
    
    
    private static IEnumerator AudioTransition(AudioSource audioSource, float duration, float targetVolume)
    {
        
        float startVolume = audioSource.volume;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}