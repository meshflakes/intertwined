using System.Collections.Generic;
using System.Linq;
using Interactable;
using UnityEngine;

namespace Character
{
    public class Interactor
    {
        public GrabbableInteractable HeldInteractable;
        private List<GameObject> _inRangeInteractables = new List<GameObject>();
        
        private float _timeBetweenInteractions = 0.2f;
        private float _nextInteractionTime = 0f;
        
        public Character PlayerChar { set; get; }

        public void UpdateHeldInteractablePosition()
        {
            if (HeldInteractable != null) HeldInteractable.UpdatePosition();
        }

        public void Interact(bool interact)
        {
            if (!interact) return;
            if (!(Time.time > _nextInteractionTime)) return;
            
            _nextInteractionTime = Time.time + _timeBetweenInteractions;

            SortInteractablesByPriority();
            
            if (HeldInteractable != null)
            {
                Debug.Log("Trying to use held interactable");
                if (TryInteractionUsingHeldInteractable()) return;
            }

            // Try to interact with any of the other 
            var i = 0;
            GameObject nextInteractionFocus = GetIthInteractionFocus(i);
            while (nextInteractionFocus != null)
            {
                if (nextInteractionFocus.GetComponent<Interactable.Interactable>().Interact(PlayerChar))
                {
                    Debug.Log($"Successful interaction with {nextInteractionFocus.name}");
                    return;
                }
                else Debug.Log($"Failed interaction with {nextInteractionFocus.name}");

                nextInteractionFocus = GetIthInteractionFocus(++i);
            }
                
            
        }

        public bool StartForcedInteraction(Interactable.Interactable interactable)
        {
            return interactable.Interact(PlayerChar);
        }

        private void SortInteractablesByPriority()
        {
            // sort _inRangeInteractables by defined priority in descending order 
            _inRangeInteractables.Sort((x, y) => 
                -x.GetComponent<Interactable.Interactable>().interactablePriority.CompareTo(
                    y.GetComponent<Interactable.Interactable>().interactablePriority));
        }

        private bool TryInteractionUsingHeldInteractable()
        {
            var i = 0;
            GameObject nextInteractionFocus = GetIthInteractionFocus(i);
            while (nextInteractionFocus != null)
            {
                Interactable.Interactable interactable =
                    nextInteractionFocus.GetComponent<Interactable.Interactable>();
                
                // skip if target interactable is itself
                if (interactable != HeldInteractable && interactable.UsedWith(HeldInteractable))
                {
                    interactable.Interact(PlayerChar, HeldInteractable); 
                    return true;
                    
                }
                nextInteractionFocus = GetIthInteractionFocus(++i);
            }
            
            // no interactable in range to use with held item, release
            HeldInteractable.Release();
            HeldInteractable = null;
            return true;
        }

        private GameObject GetIthInteractionFocus(int i)
        {
            // TODO: come up with more sophisticated logic for interaction
            if (i < _inRangeInteractables.Count)
            {
                Debug.Log($"retrieved Interactable: {_inRangeInteractables[i]}, {_inRangeInteractables[i].name}");
                return _inRangeInteractables[i];
            }
            else
            {
                return null;
            }
        }

        public void AddToInteractablesList(GameObject obj)
        {
            _inRangeInteractables.Add(obj);
        }

        public void RemoveFromInteractablesList(GameObject obj)
        {
            _inRangeInteractables.RemoveAll(gameObj => gameObj == obj);
        }

        public bool HasInteractables()
        {
            return _inRangeInteractables.Any();
        }
    }
}