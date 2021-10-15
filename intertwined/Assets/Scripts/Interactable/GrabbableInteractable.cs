using System;
using UnityEngine;

namespace Interactable
{
    public abstract class GrabbableInteractable : Interactable
    {
        private Character.Character _heldByCharacter;
        private Transform _heldByTransform;

        private bool _hasRigidBody;
        private Rigidbody _rigidbody;
        private Transform _transform;

        private Vector3 _holdingOffset = new Vector3(1, 1, 0);
        
        // TODO: may need to add holding positions / animations
        
        protected void Start()
        {
            _transform = gameObject.GetComponent<Transform>();
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _hasRigidBody = _rigidbody != null;
        }

        protected bool Held()
        {
            return _heldByCharacter != null;
        }

        public void Grab(GameObject obj)
        {
            Debug.Log("grabbing object");
            
            _heldByCharacter = obj.GetComponent<Character.Character>();
            _heldByTransform = obj.GetComponent<Transform>();
            
            _heldByCharacter.CharInteractor.HeldInteractable = this;
            
            if (_hasRigidBody)
            {
                _rigidbody.freezeRotation = true;
                _rigidbody.position = _heldByTransform.position + _holdingOffset;
                _rigidbody.useGravity = false;
            }
        }

        public void Release()
        {
            _heldByCharacter.CharInteractor.HeldInteractable = null;
            _heldByCharacter = null;
            _heldByTransform = null;
            
            if (_hasRigidBody)
            {
                _rigidbody.freezeRotation = false;
                _rigidbody.useGravity = true;
            }
        }

        public void UpdatePosition()
        {
            if (_hasRigidBody)
            {
                _rigidbody.MovePosition(_heldByTransform.position + _holdingOffset);
            }
            else
            {
                _transform.position = _heldByTransform.position + _holdingOffset;
            }
        }
    }
}