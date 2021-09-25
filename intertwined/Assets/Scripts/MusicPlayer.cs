using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource ASL1;
    public AudioSource ASL2;
    public AudioSource ASL3;

    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;
    
    // Start is called before the first frame update
    void Start()
    {
        ASL1 = gameObject.AddComponent<AudioSource>();
        ASL2 = gameObject.AddComponent<AudioSource>();
        ASL3 = gameObject.AddComponent<AudioSource>();
        
        ASL1.PlayOneShot(Level1);
        ASL1.volume = 1;
        ASL2.PlayOneShot(Level2);
        ASL2.volume = 0;
        ASL3.PlayOneShot(Level3);
        ASL3.volume = 0;
        
        ASL1.loop = true;
        ASL2.loop = true;
        ASL3.loop = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
