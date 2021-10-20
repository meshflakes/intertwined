using System;
using UnityEngine;

namespace Interactable
{
    public class DirtPileInteraction : Interactable
    {
        public GameObject plank;
        
        public override bool Interact(Character.Character interacter)
        {
            var transform1 = transform;
            Instantiate(plank, transform1.position, transform1.rotation);
            Destroy(gameObject);

            return true;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }
    }
}