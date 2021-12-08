using Prompts;
using UnityEngine;

namespace Interactable
{
    [RequireComponent(typeof(AudioSource))]
    public class LockedDoorInteraction : Interactable
    {
        public PromptManager promptManager;
        public AudioSource doorLockedAudio;

        protected void Start()
        {
            if (!doorLockedAudio)
                doorLockedAudio = GetComponent<AudioSource>();
        }

        public override bool Interact(Character.Character interacter)
        {
            if (!doorLockedAudio.isPlaying) doorLockedAudio.Play();
            promptManager.RegisterNewPrompt(interacter.charType, 5f, PromptType.X);

            return false;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }
    }
}