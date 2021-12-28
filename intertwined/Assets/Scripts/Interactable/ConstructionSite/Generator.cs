using Prompts;
using UnityEngine;

namespace Interactable.ConstructionSite
{
    public class Generator : Interactable
    {
        public int lockId;
        public Elevator.Elevator elevator;
        public Crane.Crane crane;
        public Light generatorLight;
        public PromptManager promptManager;
        
        private bool _powered = false;
        
        public override bool Interact(Character.Character interacter)
        {
            if (_powered) return false;
            
            promptManager.RegisterNewPrompt(interacter.charType, 5f, PromptType.Gas);
            return false;
        }

        public override bool Interact(Character.Character interacter, Interactable interactable)
        {
            if (_powered) return false;
            
            if (interactable is KeyType key && key.CanUnlock(lockId))
            {
                _powered = true;
                elevator.Powered = true;
                crane.Powered = true;
                generatorLight.intensity = 4f;
                return true;
            }

            return Interact(interacter);
        }

        public override bool UsedWith(Interactable other)
        {
            return true;
        }
    }
}