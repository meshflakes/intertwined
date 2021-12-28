using System;
using UnityEngine;

namespace Interactable.ConstructionSite.Crane
{
    public class CraneButton : Interactable
    {
        private Crane _crane;
        protected void Start()
        {
            _crane = GetComponentInParent<Crane>();
        }

        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            if (enteredTrigger) _crane.NumInTrigger++;
            else _crane.NumInTrigger--;
        }
    }
}