using Prompts;
using UnityEngine;

namespace Interactable
{
    public class RiverBlocker : Interactable
    {
        public Vector3 offsetAfterUnblocking = new Vector3(0, -0.2f, 0);
        private bool _currentlyBlocking = true;

        [Tooltip("Id of key required to open the door, 0 if no key is required")]
        public int lockId = 2;

        public PromptManager promptManager;
        public override bool Interact(Character.Character interacter)
        {
            promptManager.RegisterNewPrompt(Character.CharType.Boy, 2f, Prompts.PromptType.Drowning);
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

        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            if (!_currentlyBlocking || !enteredTrigger || interacter.CharInteractor.HeldInteractable != null) return;

            Interact(interacter);
        }
    }
}