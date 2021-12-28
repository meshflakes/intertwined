using Interactable.Util;
using UnityEngine;

namespace Interactable
{
    public class SignInteractable : Interactable
    {
        [Header("Camera Sequence")]
        [Tooltip("Time taken for camera to move from original position to end position and vice versa")]
        public float animationDuration = 1;
        [Tooltip("Time spent viewing the end position")]
        public float minimumTimeBeforeReturn = 3;
        
        private bool _cameraSequencePlayed = false;

        private CameraSequenceManager _cameraSequence;
        protected void Start()
        {
            var targetTransform = transform.Find("Camera");
            _cameraSequence = new CameraSequenceManager(targetTransform, animationDuration, minimumTimeBeforeReturn);
        }

        public override bool Interact(Character.Character interacter)
        {
            // TODO: remove player control? 
            _cameraSequence.StartNewCameraSequence();
            _cameraSequencePlayed = true;
            return true;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }

        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            if (!enteredTrigger || _cameraSequencePlayed) return;

            Interact(interacter);
        }
    }
}