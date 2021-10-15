using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class KeyInteractable : GrabbableInteractable, KeyType
    {
        [Tooltip("List of lock ids that this key can unlock")]
        public List<int> unlockableLockIds;

        private HashSet<int> _unlockableLockIdsSet;

        public new void Start()
        {
            base.Start();
            if (unlockableLockIds != null)
            {
                _unlockableLockIdsSet = new HashSet<int>(unlockableLockIds);
            }
            else
            {
                _unlockableLockIdsSet = new HashSet<int>();
            }
        }

        public bool CanUnlock(int lockId)
        {
            return _unlockableLockIdsSet.Contains(lockId);
        }

        public override bool UsedWith(Interactable other)
        {
            return true;
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
    }
}