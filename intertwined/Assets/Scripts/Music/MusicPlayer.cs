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
    public AudioSource ASL6;

    // Start is called before the first frame update
    void Start()
    {
        ASL1.Play();
        ASL2.Play();
        ASL3.Play();
        ASL4.Play();
        ASL5.Play();
        ASL6.Play();
        ASL1.volume = 1;
    }
    
    public void playLevelOne()
    {
        ASL2.volume = 1;
        ASL3.volume = 0;
        ASL4.volume = 0;
        ASL5.volume = 0;
        ASL6.volume = 0;
    }

    public void playLevelTwo()
    {
        ASL2.volume = 1;
        ASL3.volume = 1;
        ASL4.volume = 0;
        ASL5.volume = 0;
        ASL6.volume = 0;
    }

    public void playLevelThree()
    {
        ASL2.volume = 1;
        ASL3.volume = 1;
        ASL4.volume = 1;
        ASL5.volume = 0;
        ASL6.volume = 0;
    }

    public void playLevelFour()
    {
        ASL2.volume = 1;
        ASL3.volume = 1;
        ASL4.volume = 1;
        ASL5.volume = 1;
        ASL6.volume = 0;
    }

    public void playLevelFive()
    {
        ASL2.volume = 1;
        ASL3.volume = 1;
        ASL4.volume = 1;
        ASL5.volume = 1;
        ASL6.volume = 0.6f;
    }
}