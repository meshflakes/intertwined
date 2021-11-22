using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactable
{
    [RequireComponent(typeof(AudioSource))]
    public class DoorInteraction : Interactable
    {
        private Transform _transform;
        private Vector3 _pivotPosition;
        private bool Swinging => _remainingRotation != 0;
        private float _remainingRotation = 0f;
        private Vector3 _rotationAxis = Vector3.zero;
        private int _rotationAxisModifier;

        [Header("Door")] [Tooltip("How many degrees the door opens on rotation")] [Range(0, 359)]
        public int rotation = 90;

        [Tooltip("How fast the door rotates")] public int degreesRotatedPerSecond = 60;

        [Space(10)] [Tooltip("Starting 'unlocked' status of the door")]
        public bool unlocked = true;

        [Tooltip("Id of key required to open the door, 0 if no key is required")]
        public int lockId;

        [Space(10)] [Tooltip("Starting state of the door")]
        public bool open = false;

        [Tooltip("Toggle direction of opening the door")] [FormerlySerializedAs("openingTowardsNorth")]
        public bool openClockwise = true;

        public bool canBeClosedAfterOpening = false;

        [Header("Audio")]
        public AudioSource doorOpenAudio;
        public AudioSource unlockAudio;
        public AudioSource doorLockedAudio;

        protected void Start()
        {
            _transform = gameObject.GetComponent<Transform>();
            _pivotPosition = _transform.Find("DoorPivot").position;
            _rotationAxisModifier = openClockwise ? 1 : -1;
        }

        protected void Update()
        {
            if (!Swinging) return;

            var degreesToRotate = Math.Min(Time.deltaTime * degreesRotatedPerSecond, _remainingRotation);
            _transform.RotateAround(_pivotPosition, _rotationAxis, degreesToRotate);
            _remainingRotation -= degreesToRotate;
        }

        public override bool Interact(Character.Character interacter)
        {
            if (!unlocked || Swinging) return false;

            if (!open)
            {
                if (doorOpenAudio != null && !doorOpenAudio.isPlaying) doorOpenAudio.Play();
                open = true;
                _remainingRotation = rotation;
                _rotationAxis = Vector3.up * _rotationAxisModifier;
                return true;
            }
            else if (canBeClosedAfterOpening)
            {
                open = false;
                _remainingRotation = rotation;
                _rotationAxis = Vector3.down * _rotationAxisModifier;
                return true;
            }

            return false;
        }

        public override bool Interact(Character.Character interacter, Interactable interactable)
        {
            if (lockId == 0 || unlocked) return Interact(interacter);

            if (interactable is KeyType key && key.CanUnlock(lockId))
            {
                if (unlockAudio != null && !unlockAudio.isPlaying) unlockAudio.Play();
                unlocked = true;
                return Interact(interacter);
            }
            else
            {
                if (!doorLockedAudio.isPlaying) doorLockedAudio.Play();
                return false;
            }
        }

        public override bool UsedWith(Interactable other)
        {
            return true;
        }
    }
}