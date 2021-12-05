using System;
using UnityEngine;

namespace Interactable.Ladder
{
    /**
     * Moderate climbing and grabbing interactions of the ladder
     *
     * Setup: 
     */
    public class LadderManager : MonoBehaviour
    {
        private GrabbableInteractable _ladderGrabbable;
        private LadderClimbable _ladderClimbable;

        public bool CanClimb => _beingHeld;
        public bool CanGrab => _beingClimbed;

        private bool _beingClimbed = false;
        private bool _beingHeld = false;

        protected void Start()
        {
            _ladderGrabbable = GetComponent<GrabbableInteractable>();
            _ladderClimbable = GetComponentInChildren<LadderClimbable>();
        }

        public void StartedClimbing()
        {
            _beingClimbed = true;
            _ladderGrabbable.interactableEnabled = false;
        }

        public void StartedGrabbing()
        {
            _beingHeld = true;
            _ladderClimbable.ClimbingDisabled = true;
        }

        public void StoppedGrabbing()
        {
            
            _ladderClimbable.ClimbingDisabled = false;
            Debug.Log("Stopped holding, re-enabling climbing. ClimbingDisabled = " + _ladderClimbable.ClimbingDisabled);
            _beingHeld = false;
        }

        public void StoppedClimbing()
        {
            _beingClimbed = false;
            _ladderGrabbable.interactableEnabled = true;
        }
    }
}