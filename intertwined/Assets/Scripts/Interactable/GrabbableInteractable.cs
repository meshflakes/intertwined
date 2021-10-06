using System;
using UnityEngine;

namespace Interactable
{
    public abstract class GrabbableInteractable : Interactable
    {
        private GameObject _heldBy;
        private Rigidbody _rigidbody;

        private Vector3 _holdingOffset = new Vector3(3, 3, 0);
        
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
            _rigidbody.useGravity = false;
        }

        public void Release()
        {
            _heldBy.GetComponent<Character.Character>().CharInteractor.HeldInteractable = null;
            _heldBy = null;
            _rigidbody.freezeRotation = false;
            _rigidbody.useGravity = true;
        }

        public void UpdatePosition()
        {
            _rigidbody.MovePosition(_heldBy.GetComponent<Transform>().position + _holdingOffset);
        }
    }
}