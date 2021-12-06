using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class KeyInteractable : GrabbableInteractable, KeyType
    {
        [Header("Key")]
        [Tooltip("List of lock ids that this key can unlock")]
        public List<int> unlockableLockIds = new List<int>();

        private HashSet<int> _unlockableLockIdsSet;
        private Animator _anim;
        public new void Start()
        {
            base.Start();
            _unlockableLockIdsSet = new HashSet<int>(unlockableLockIds);
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
            _anim = interacter.GetComponentInChildren<Animator>();
            if (Held())
            {
                _anim.SetInteger("Interacting", 0);
                Debug.Log("Releasing Object");
                Release();
            }
            else
            {
                _anim.SetInteger("Interacting", 1);
                Debug.Log("Grabbing object");
                Grab(interacter.gameObject);
            }

            return true;
        }
    }
}