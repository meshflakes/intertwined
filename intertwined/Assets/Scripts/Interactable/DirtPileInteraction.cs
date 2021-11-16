using System;
using UnityEngine;

namespace Interactable
{
    public class DirtPileInteraction : Interactable
    {
        public GameObject plank;
        
        public override bool Interact(Character.Character interacter)
        {
            var plankChildTransform = transform.Find("Plank");
            
            Instantiate(plank, plankChildTransform.position, plankChildTransform.rotation);
            RemoveInteractableFromCharacters();
            Destroy(gameObject);
            
            return true;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }
    }
}