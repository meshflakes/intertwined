using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class PlankInteractable : GrabbableInteractable, KeyType
    {
        
        [Tooltip("List of lock ids that this key can unlock")]
        public List<int> unlockableLockIds = new List<int>();

        private HashSet<int> _unlockableLockIdsSet;
        private Vector3 _releasePosition = new Vector3(-20.25f, -1.1732f, -28.35f);

        public new void Start()
        {
            base.Start();
            _unlockableLockIdsSet = new HashSet<int>(unlockableLockIds);
            _unlockableLockIdsSet.Add(2);
        }
        
        public bool CanUnlock(int lockId)
        {
            return _unlockableLockIdsSet.Contains(lockId);
        }
        
        public override bool Interact(Character.Character interacter)
        {
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
            
            // TODO: figure out what's wrong here
            // if (TryGetComponent(out Rigidbody rigidbody))
            // {
            //     rigidbody.isKinematic = true;
            //     rigidbody.MovePosition(_releasePosition);
            //     rigidbody.isKinematic = false;
            //     rigidbody.velocity = Vector3.zero;
            // }
            // else
            // {
            //     transform.position = _releasePosition;
            // }
        }
    }
}