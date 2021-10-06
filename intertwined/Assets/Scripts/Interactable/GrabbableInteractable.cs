using System;
using UnityEngine;

namespace Interactable
{
    public abstract class GrabbableInteractable : Interactable
    {
        private GameObject _heldBy = null;
        private Rigidbody _rigidbody;

        private Vector3 _holdingOffset = Vector3.forward;
        
        // TODO: may need to add holding positions / animations
        
        private void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        protected bool Held()
        {
            return _heldBy != null;
        }

        public void Grab(GameObject obj)
        {
            _heldBy = obj;
            obj.GetComponent<Character.Character>().CharInteractor.HeldInteractable = this;
            _rigidbody.freezeRotation = true;
            _rigidbody.position = _heldBy.GetComponent<Transform>().position + _holdingOffset;
        }

        public void Release()
        {
            _heldBy.GetComponent<Character.Character>().CharInteractor.HeldInteractable = null;
            _heldBy = null;
            _rigidbody.freezeRotation = false;
        }

        public void UpdatePosition()
        {
            _rigidbody.MovePosition(_heldBy.GetComponent<Transform>().position + _holdingOffset);
        }
    }
}