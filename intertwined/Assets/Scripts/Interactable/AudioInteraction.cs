using UnityEngine;

namespace Interactable
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioInteraction : Interactable
    {
        public AudioSource doorLockedAudio;

        protected void Start()
        {
            if (!doorLockedAudio)
                doorLockedAudio = GetComponent<AudioSource>();
        }

        public override bool Interact(Character.Character interacter)
        {
            if (!doorLockedAudio.isPlaying) doorLockedAudio.Play();

            return false;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }
    }
}