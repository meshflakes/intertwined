using System;

namespace Interactable.ConstructionSite.Elevator
{
    public class ElevatorButton : Interactable
    {
        public bool upButton;
        private int DeltaLevel => upButton ? 1 : -1;
        private Elevator _elevator;
        
        protected void Reset()
        {
            interactablePriority = -1;
        }

        
        protected void Start()
        {
            _elevator = GetComponentInParent<Elevator>();
        }

        public override bool Interact(Character.Character interacter)
        {
            return _elevator.TryChangeFloor(interacter, DeltaLevel);
        }
    }
}