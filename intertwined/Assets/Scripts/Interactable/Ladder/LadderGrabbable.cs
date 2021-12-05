using System;
using UnityEngine;

namespace Interactable.Ladder
{
    public class LadderGrabbable : GrabbableInteractable
    {
        private LadderManager _ladderManager;

        protected new void Start()
        {
            base.Start();
            _ladderManager = GetComponent<LadderManager>();
        }
        
        public override bool UsedWith(Interactable other)
        {
            return false;
        }
        
        public override bool Interact(Character.Character interacter)
        {
            if (!interactableEnabled)
            {
                Debug.Log("Ladder disabled, cannot grab");
                return false;
            }
            
            Debug.Log("Grabbing Ladder");
            
            _ladderManager.StartedGrabbing();
            Grab(interacter.gameObject);
            
            return true;
        }

        public override void Release()
        {
            Debug.Log("Releasing Ladder");
            _ladderManager.StoppedGrabbing();
            base.Release();
        }
    }
}