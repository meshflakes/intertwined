using UnityEngine;

namespace Interactable.ConstructionSite
{
    public class Generator : Interactable
    {
        public int lockId;
        public Elevator elevator;
        public Crane crane;
        public Light generatorLight;
        
        private bool _powered = false;
        
        public override bool Interact(Character.Character interacter)
        {
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
                generatorLight.intensity = 2f;
                return Interact(interacter);
            }

            return false;
        }

        public override bool UsedWith(Interactable other)
        {
            return true;
        }
    }
}