using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        [Tooltip("Allows the dog to interact with this Interactable")]
        public bool dogCanInteract;

        [Tooltip("Allows the boy to interact with this Interactable")]
        public bool boyCanInteract;

        [FormerlySerializedAs("Enabled")] [HideInInspector]
        public bool interactableEnabled = true;

        private List<Character.Character> _characters;
        
        public abstract bool Interact(Character.Character interacter);

        public virtual bool Interact(Character.Character interacter, Interactable interactable)
        {
            return interactableEnabled && Interact(interacter);
        }

        /**
         * Used to check if an interactable object is used with another
         * (e.g. a key with a door)
         * 
         * This allows prioritization of interacting with an object that
         * the player has an item for instead of e.g. doing it randomly
         */
        public abstract bool UsedWith(Interactable other);
        
        protected void OnTriggerEnter(Collider other)
        {
            // Add itself to the character's interactables list
            if (CollidingObjectCanInteract(other))
            {
                other.gameObject.GetComponentInParent<Character.Character>()
                    .CharInteractor.AddToInteractablesList(gameObject);
                Debug.Log($"adding {gameObject.name} to list: {gameObject}");
            }
            
            
        }
        
        
    
        protected void OnTriggerExit(Collider other)
        {
            // Remove itself to the character's interactables list
            if (CollidingObjectCanInteract(other))
            {
                other.gameObject.GetComponentInParent<Character.Character>()
                    .CharInteractor.RemoveFromInteractablesList(gameObject);
                Debug.Log($"removing {gameObject.name} from list");
            }
        }

        private bool CollidingObjectCanInteract(Component other)
        {
            return interactableEnabled &&
                  (boyCanInteract && (other.CompareTag("Boy") || other.CompareTag("BoySubObjects"))
                || dogCanInteract && (other.CompareTag("Dog") || other.CompareTag("DogSubObjects")));
        }

        protected void RemoveInteractableFromCharacters()
        {
            GameObject.FindWithTag("Boy").GetComponentInParent<Character.Character>().CharInteractor
                .RemoveFromInteractablesList(gameObject);
            GameObject.FindWithTag("Dog").GetComponentInParent<Character.Character>().CharInteractor
                .RemoveFromInteractablesList(gameObject);
        }
    }
}