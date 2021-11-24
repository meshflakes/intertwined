using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactable
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactable : MonoBehaviour
    {
        [Tooltip("Allows the dog to interact with this Interactable")]
        public bool dogCanInteract;

        [Tooltip("Allows the boy to interact with this Interactable")]
        public bool boyCanInteract;

        [FormerlySerializedAs("Enabled")] [HideInInspector]
        public bool interactableEnabled = true;

        private List<Character.Character> _characters;
        private Animator anim; 

        protected void OnValidate()
        {
            foreach (var interactableCollider in GetComponents<Collider>())
            {
                if (interactableCollider.isTrigger) return;
            }

            throw new ArgumentException($"No trigger collider on interactable: {gameObject.name}");
        }

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
                //TODO clean up this. It is not a good way to handle exiting the plank hold animation
                other.gameObject.GetComponentInParent<Character.Character>().GetComponentInChildren<Animator>().SetInteger("Interacting", 0);
                Debug.Log($"adding {gameObject.name} to list");
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