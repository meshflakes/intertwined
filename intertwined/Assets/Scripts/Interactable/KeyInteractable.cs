﻿namespace Interactable
{
    public class KeyInteractable : GrabbableInteractable, KeyType
    {
        public bool CanUnlock(int keyId)
        {
            // TODO: add proper logic for unlock
            return keyId != 0;
        }

        public override bool UsedWith(Interactable other)
        {
            return true;
        }

        public override bool Interact(Character.Character interacter)
        {
            if (Held())
            {
                Release();
            }
            else
            {
                Grab(interacter.gameObject);
            }

            return true;
        }
    }
}