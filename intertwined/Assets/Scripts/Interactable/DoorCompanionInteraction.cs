using Prompts;
using UnityEngine;

namespace Interactable
{
    [RequireComponent(typeof(AudioSource))]
    public class DoorCompanionInteraction : Interactable
    {
        public PromptManager promptManager;
        public AudioSource doorLockedAudio;
        private DoorInteraction _door;

        protected void Start()
        {
            if (!doorLockedAudio)
                doorLockedAudio = GetComponent<AudioSource>();

            _door = transform.parent.GetComponentInChildren<DoorInteraction>();
        }

        public override bool Interact(Character.Character interacter)
        {
            if (_door.open) return false;
            
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