using System;
using System.Collections.Generic;
using Prompts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Interactable
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interactable : MonoBehaviour
    {
        [Tooltip("Bit flag of valid interactions")]
        public Interaction interactions;

        [FormerlySerializedAs("Enabled")] [HideInInspector]
        public bool interactableEnabled = true;

        [Tooltip("Sets the priority level of an interactable for when multiple interactables are in range")]
        public float interactablePriority;
        
        private bool BoyCanInteract => ((int) interactions & (int) InteractionMasks.Boy) > 0;
        private bool DogCanInteract => ((int) interactions & (int) InteractionMasks.Dog) > 0;
        private bool ValidClickInteraction => ((int) interactions & (int) InteractionMasks.Click) > 0;
        private bool ValidProximityInteraction => ((int) interactions & (int) InteractionMasks.Proximity) > 0;

        protected void OnValidate()
        {
            foreach (var interactableCollider in GetComponents<Collider>())
            {
                if (interactableCollider.isTrigger) return;
            }
            
            foreach (var interactableCollider in GetComponentsInChildren<Collider>())
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

        protected virtual void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            throw new NotImplementedException($"ProximityInteraction called but no implementation exists for {gameObject.name}");
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
            var validInteractions = GetValidInteractions(other);
            
            // if (!CollidingObjectCanInteract(other)) return;
            if (validInteractions == 0) return;
            
            var interacter = other.gameObject.GetComponentInParent<Character.Character>();
            
            if ((validInteractions & (int) InteractionMasks.Click) > 0) {
                interacter.CharInteractor.AddToInteractablesList(gameObject);
				
                //TODO clean up this. It is not a good way to handle exiting the plank hold animation
                other.gameObject.GetComponentInParent<Character.Character>().GetComponentInChildren<Animator>().SetInteger("Interacting", 0);

                Debug.Log($"adding {gameObject.name} to list");
            }
            
            if ((validInteractions & (int) InteractionMasks.Proximity) > 0)
                ProximityInteraction(interacter, true);
        }
        
        protected void OnTriggerExit(Collider other)
        {
            var validInteractions = GetValidInteractions(other);
            
            // if (!CollidingObjectCanInteract(other)) return;
            if (validInteractions == 0) return;
            
            var interacter = other.gameObject.GetComponentInParent<Character.Character>();
            
            if ((validInteractions & (int) InteractionMasks.Click) > 0) {
                interacter.CharInteractor.RemoveFromInteractablesList(gameObject);
                Debug.Log($"removing {gameObject.name} from list");
            }
            
            if ((validInteractions & (int) InteractionMasks.Proximity) > 0)
                ProximityInteraction(interacter, false);
        }

        /**
         * mask interactions with the character that's trying to interact
         * so only valid interactions for the interacting character are left
         */
        private int GetValidInteractions(Component other)
        {
            if (!interactableEnabled) return 0;
            
            if (other.CompareTag("Boy") || other.CompareTag("BoySubObjects"))
                return (int) interactions & (int) InteractionMasks.Boy;
            
            if (other.CompareTag("Dog") || other.CompareTag("DogSubObjects"))
                return (int) interactions & (int) InteractionMasks.Dog;

            return 0;
        }
        
        private bool CollidingObjectCanInteract(Component other)
        {
            return interactableEnabled &&
                  (BoyCanInteract && (other.CompareTag("Boy") || other.CompareTag("BoySubObjects"))
                   || DogCanInteract && (other.CompareTag("Dog") || other.CompareTag("DogSubObjects")));
        }

        protected void RemoveInteractableFromCharacters()
        {
            GameObject.FindWithTag("Boy").GetComponentInParent<Character.Character>().CharInteractor
                .RemoveFromInteractablesList(gameObject);
            GameObject.FindWithTag("Dog").GetComponentInParent<Character.Character>().CharInteractor
                .RemoveFromInteractablesList(gameObject);
        }
    }

    [Flags] public enum Interaction
    {
        BoyClick = 1 << 0,
        BoyProximity = 1 << 1,
        DogClick = 1 << 2,
        DogProximity = 1 << 3
    }

    public enum InteractionMasks
    {
        Boy = Interaction.BoyClick | Interaction.BoyProximity,
        Dog = Interaction.DogClick | Interaction.DogProximity,
        Click = Interaction.BoyClick | Interaction.DogClick,
        Proximity = Interaction.BoyProximity | Interaction.DogProximity,
    }
}