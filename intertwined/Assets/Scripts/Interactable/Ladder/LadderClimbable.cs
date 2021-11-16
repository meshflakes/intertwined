using UnityEngine;

namespace Interactable.Ladder
{
    public class LadderClimbable : ClimbableObj
    {
        private LadderManager _ladderManager;
        private Rigidbody _rigidbody;
        
        protected new void Start()
        {
            base.Start();
            _ladderManager = gameObject.GetComponentInParent<LadderManager>();

            _rigidbody = GetComponentInParent<Rigidbody>();
        }

        protected new void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            
            if (BeingClimbed) _ladderManager.StartedClimbing();
            
        }
        protected new void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            
            if (CollidingObjectCanInteract(other) && !BeingClimbed) _ladderManager.StoppedClimbing();
        }
    }
}