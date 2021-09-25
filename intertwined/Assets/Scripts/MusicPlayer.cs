using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource ASAx1;
    public AudioClip Ax1;
    public AudioSource ASAx2;
    public AudioClip Ax2;
    public AudioSource ASAx3;
    public AudioClip Ax3;
    // Start is called before the first frame update
    void Start()
    {
        ASAx1 = gameObject.AddComponent<AudioSource>();
        ASAx2 = gameObject.AddComponent<AudioSource>();
        ASAx3 = gameObject.AddComponent<AudioSource>();
        
        ASAx1.PlayOneShot(Ax1);
        
        ASAx2.PlayOneShot(Ax2);
        ASAx2.volume = 0;
        ASAx3.PlayOneShot(Ax3);
        ASAx2.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
