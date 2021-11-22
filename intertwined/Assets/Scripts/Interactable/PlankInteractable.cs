using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class PlankInteractable : GrabbableInteractable, KeyType
    {
        
        [Tooltip("List of lock ids that this key can unlock")]
        public List<int> unlockableLockIds = new List<int>();

        private HashSet<int> _unlockableLockIdsSet;
        private readonly Vector3 _releasePosition = new Vector3(-20.72944f, 1.130294f, -28.1477f);
        private readonly Vector3 _releaseAngle = new Vector3(0.014f, 117.748f, -59.471f);
        private bool _droppingToFinalPosition = false;
        private readonly Vector3 _finalPosition = new Vector3(-20.42524f, 0.087f, -27.59458f);
        private readonly Vector3 _finalAngle = new Vector3(0, 118.087f, 0);

        public AudioSource plankFallAudio;
        private bool _plankFallingAudioPlayed = false;

        private bool _inFinalPosition = false;

        public new void Start()
        {
            base.Start();
            _unlockableLockIdsSet = new HashSet<int>(unlockableLockIds);
            _unlockableLockIdsSet.Add(2);
        }

        protected new void OnValidate()
        {
            base.OnValidate();
            if (unlockableLockIds.Count == 0)
                throw new ArgumentException("Unlockable Lock Ids must contain at least 1 element");
        }

        public void Update()
        {
            if (!_inFinalPosition && _droppingToFinalPosition)
            {
                var z = GrabbableTransform.rotation.eulerAngles.z;
                if (!_plankFallingAudioPlayed && (z > 330 || z < 30))
                {
                    plankFallAudio.Play();
                    _plankFallingAudioPlayed = true;
                }
                if (z > 358.5f || z < 2)
                {
                    _droppingToFinalPosition = false;
                    
                    Destroy(GrabbableRigidbody);
                    GrabbableTransform.position = _finalPosition;
                    GrabbableTransform.rotation = Quaternion.Euler(_finalAngle);
                    RemoveInteractableFromCharacters();
                    _inFinalPosition = true;
                    enabled = false;
                }
            }
        }

        public bool CanUnlock(int lockId)
        {
            return _unlockableLockIdsSet.Contains(lockId);
        }
        
        public override bool Interact(Character.Character interacter)
        {
            if (_inFinalPosition) return false;
            
            if (Held())
            {
                Debug.Log("Releasing Object");
                Release();
            }
            else
            {
                Debug.Log("Grabbing object");
                Grab(interacter.gameObject);
            }

            return true;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }

        public void AidedRelease()
        {
            base.Release();
            
            GrabbableRigidbody.MovePosition(_releasePosition);
            GrabbableRigidbody.MoveRotation(Quaternion.Euler(_releaseAngle));

            _droppingToFinalPosition = true;
        }
    }
}