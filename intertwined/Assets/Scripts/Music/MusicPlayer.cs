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

    public void playParkTracks(int theme1, int theme2, int theme3, int theme4, int theme5, int ambience1, int ambience2,
        int ambience3)
    {
        StartCoroutine(AudioTransition(ASL1, TRANSITION_DURATION, theme1));
        StartCoroutine(AudioTransition(ASL2, TRANSITION_DURATION, theme2));
        StartCoroutine(AudioTransition(ASL3, TRANSITION_DURATION, theme3));
        StartCoroutine(AudioTransition(ASL4, TRANSITION_DURATION, theme4));
        StartCoroutine(AudioTransition(ASL5, TRANSITION_DURATION, theme5));
        StartCoroutine(AudioTransition(ParkAmbience1, TRANSITION_DURATION, ambience1));
        StartCoroutine(AudioTransition(ParkAmbience2, TRANSITION_DURATION, ambience2));
        StartCoroutine(AudioTransition(ParkAmbience3, TRANSITION_DURATION, ambience3));
    }

    public void playParkLevelTracks(int level)
    {
        int[] volumes;
        parkLevelMappings.TryGetValue(level, out volumes);
        playParkTracks(volumes[0], volumes[1], volumes[2], volumes[3], volumes[4], volumes[5], volumes[6], volumes[7]);
    }
    
    public void playLevelOne()
    {
        playParkTracks(1, 1, 0, 0, 0, 1, 1, 0);
        /*
        StartCoroutine(AudioTransition(ASL2, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL3, TRANSITION_DURATION, 0));
        StartCoroutine(AudioTransition(ASL4, TRANSITION_DURATION, 0));
        StartCoroutine(AudioTransition(ASL5, TRANSITION_DURATION, 0));
        StartCoroutine(AudioTransition(ParkAmbience2, TRANSITION_DURATION, 1));*/

    }

    public void playLevelTwo()
    {
        playParkTracks(1, 1, 1, 0, 0, 1, 1, 1);
            /*
        StartCoroutine(AudioTransition(ASL2, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL3, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL4, TRANSITION_DURATION, 0));
        StartCoroutine(AudioTransition(ASL5, TRANSITION_DURATION, 0));
        StartCoroutine(AudioTransition(ParkAmbience3, TRANSITION_DURATION, 1));*/

    }

    public void playLevelThree()
    {
        playParkTracks(1, 1, 1, 1, 0, 1, 1, 1);
        /*
        StartCoroutine(AudioTransition(ASL2, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL3, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL4, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL5, TRANSITION_DURATION, 0));*/

    }

    public void playLevelFour()
    {
        playParkTracks(1, 1, 1, 1, 1, 1, 1, 1);
        /*
        StartCoroutine(AudioTransition(ASL2, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL3, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL4, TRANSITION_DURATION, 1));
        StartCoroutine(AudioTransition(ASL5, TRANSITION_DURATION, 0.5f));*/

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