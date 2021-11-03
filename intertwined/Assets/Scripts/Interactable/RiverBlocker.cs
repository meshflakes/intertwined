﻿using UnityEngine;

namespace Interactable
{
    public class RiverBlocker : Interactable
    {
        public Vector3 offsetAfterUnblocking = new Vector3(0, -0.2f, 0);
        private bool _currentlyBlocking = true;

        [Tooltip("Id of key required to open the door, 0 if no key is required")]
        public int lockId = 2;

        public override bool Interact(Character.Character interacter)
        {
            return false;
        }

        public override bool Interact(Character.Character interacter, Interactable interactable)
        {
            if (!_currentlyBlocking) return false;

            if (interactable is KeyType key && key.CanUnlock(lockId))
            {
                transform.position += offsetAfterUnblocking;
                _currentlyBlocking = false;

                if (interactable is PlankInteractable plank)
                {
                    plank.AidedRelease();
                }
                
                return true;
            }

            return false;
        }
        
        public override bool UsedWith(Interactable other)
        {
            return true;
        }
    }
}