﻿using System.Collections.Generic;
using Interactable;
using UnityEngine;

namespace Character
{
    public class Interactor
    {
        private List<GameObject> _inRangeInteractables = new List<GameObject>();
        public GrabbableInteractable HeldInteractable;
        
        public Character PlayerChar { set; get; }

        public void UpdateHeldInteractablePosition()
        {
            if (HeldInteractable != null) HeldInteractable.UpdatePosition();
        }

        public void Interact(bool interact)
        {
            if (!interact) return;
            
            if (HeldInteractable != null)
            {
                if (TryInteractionUsingHeldInteractable()) return;
            }

            // Try to interact with any of the other 
            var i = 0;
            GameObject nextInteractionFocus = GetIthInteractionFocus(i);
            while (nextInteractionFocus != null)
            {
                if (nextInteractionFocus.GetComponent<Interactable.Interactable>().Interact(PlayerChar))
                {
                    return;
                }
                
                nextInteractionFocus = GetIthInteractionFocus(++i);
            }
                
            
        }

        private bool TryInteractionUsingHeldInteractable()
        {
            var i = 0;
            GameObject nextInteractionFocus = GetIthInteractionFocus(i);
            while (nextInteractionFocus != null)
            {
                Interactable.Interactable interactable = nextInteractionFocus.GetComponent<Interactable.Interactable>();
                if (interactable.UsedWith(HeldInteractable))
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
    }
}