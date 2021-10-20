using UnityEngine;

namespace Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        [Tooltip("Allows the dog to interact with this Interactable")]
        public bool dogCanInteract;

        [Tooltip("Allows the boy to interact with this Interactable")]
        public bool boyCanInteract;
        
        public abstract bool Interact(Character.Character interacter);

        public virtual bool Interact(Character.Character interacter, Interactable interactable)
        {
            return Interact(interacter);
        }

        /**
         * Used to check if an interactable object is used with another
         * (e.g. a key with a door)
         * 
         * This allows prioritization of interacting with an object that
         * the player has an item for instead of e.g. doing it randomly
         */
        public abstract bool UsedWith(Interactable other);
        
        private void OnTriggerEnter(Collider other)
        {
            // Add itself to the character's interactables list
            if (CollidingObjectCanInteract(other))
            {
                other.gameObject.GetComponent<Character.Character>()
                    .CharInteractor.AddToInteractablesList(gameObject);
                Debug.Log("adding to list");
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            // Remove itself to the character's interactables list
            if (CollidingObjectCanInteract(other))
            {
                other.gameObject.GetComponent<Character.Character>()
                    .CharInteractor.RemoveFromInteractablesList(gameObject);
                Debug.Log("removing from list");
            }
        }

        private bool CollidingObjectCanInteract(Collider other)
        {
            return (boyCanInteract && other.CompareTag("Boy"))
                || (dogCanInteract && other.CompareTag("Dog"));
        }
    }
}