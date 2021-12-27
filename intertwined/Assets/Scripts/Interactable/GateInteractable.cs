using Interactable.Util;
using Prompts;
using UnityEngine;

namespace Interactable
{
    public class GateInteractable : DoorInteraction
    {
        [Header("Camera Sequence")]
        [Tooltip("Time taken for camera to move from original position to end position and vice versa")]
        public float animationDuration = 1;
        [Tooltip("Time spent viewing the end position")]
        public float minimumTimeBeforeReturn = 3;

        public AudioSource unlockAndOpenAudio;
        public AudioSource lockedAudio;

        private CameraSequenceManager _cameraSequence;
        private GameObject _lock;
        
        private bool _cameraSequencePlayed = false;
        
        public PromptManager promptManager;
        protected new void Start()
        {
            base.Start();
            
            var targetTransform = transform.Find("Camera");
            _cameraSequence = new CameraSequenceManager(targetTransform, animationDuration, minimumTimeBeforeReturn);
            
            _lock = GameObject.Find("Lock");
        }

        public override bool Interact(Character.Character interacter)
        {
            if (unlocked)
            {
                unlockAndOpenAudio.Play();
                return base.Interact(interacter);
            }
            
            promptManager.RegisterNewPrompt(Character.CharType.Boy, 5f, Prompts.PromptType.Key);
            
            // TODO: remove player control? 
            PlayCameraSequence();
            return true;
        }

        public override bool Interact(Character.Character interacter, Interactable interactable)
        {
            if (!unlocked && base.Interact(interacter, interactable) && unlocked)
            {
                Destroy(_lock);
                if (!unlockAndOpenAudio.isPlaying) unlockAndOpenAudio.Play();
                return true;
            }

            return Interact(interacter);
        }

        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            if (!enteredTrigger || _cameraSequencePlayed) return;

            var heldInteractable = interacter.CharInteractor.HeldInteractable;
            if (heldInteractable != null && heldInteractable.name == "Key") return;
            
            PlayCameraSequence();
        }

        private void PlayCameraSequence()
        {
            if (!lockedAudio.isPlaying) lockedAudio.Play();
            _cameraSequence.StartNewCameraSequence();
            _cameraSequencePlayed = true;
        }
    }
}