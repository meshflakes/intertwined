using System;
using UnityEngine;

namespace Interactable.ConstructionSite
{
    public class CraneButton : Interactable
    {
        private Crane _crane;
        protected void Start()
        {
            _crane = GetComponentInParent<Crane>();
        }

        // protected void OnTriggerEnter(Collider other)
        // {
        //     if (CollidingObjectCanInteract(other)) _crane.NumInTrigger++;
        // }
        //
        // protected void OnTriggerExit(Collider other)
        // {
        //     if (CollidingObjectCanInteract(other)) _crane.NumInTrigger--;
        // }
        //
        // private bool CollidingObjectCanInteract(Component other)
        // {
        //     return (other.CompareTag("Boy") || other.CompareTag("BoySubObjects"))
        //            || (other.CompareTag("Dog") || other.CompareTag("DogSubObjects"));
        // }

        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            if (enteredTrigger) _crane.NumInTrigger++;
            else _crane.NumInTrigger--;
        }
    }
}