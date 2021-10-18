using UnityEngine;
using Character;

namespace Interactable
{
    public abstract class GrabbableInteractable : Interactable
    {
        [Header("Holding Positions")]
        [Tooltip("Offset of item when held by the boy")]
        public Vector3 boyHoldingOffset;
        [Tooltip("Angle item is held at by boy")]
        public Vector3 boyHoldingAngle;
        
        [Space(10)]
        [Tooltip("Offset of item when held by the dog")]
        public Vector3 dogHoldingOffset;
        [Tooltip("Angle item is held at by dog")]
        public Vector3 dogHoldingAngle;

        private CharType? _holdingChar;
        private Character.Character _heldByCharacter;
        private Transform _heldByTransform;

        private Quaternion _boyHoldingQuaternion;
        private Quaternion _dogHoldingQuaternion;

        private bool _hasRigidBody;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Transform _defaultParentTransform;
        
        protected void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _hasRigidBody = _rigidbody != null;
            
            _transform = gameObject.GetComponent<Transform>();
            _defaultParentTransform = _transform.parent;
            
            _boyHoldingQuaternion = Quaternion.Euler(boyHoldingAngle);
            _dogHoldingQuaternion = Quaternion.Euler(dogHoldingAngle);
        }

        protected bool Held()
        {
            return _heldByCharacter != null;
        }

        public void Grab(GameObject obj)
        {
            // Set parent to the holding char
            _transform.SetParent(_heldByTransform, true);
            _heldByTransform = obj.GetComponent<Transform>();
            
            _heldByCharacter = obj.GetComponent<Character.Character>();
            _heldByCharacter.CharInteractor.HeldInteractable = this;
            _holdingChar = obj.CompareTag("Dog") ? CharType.Dog : CharType.Boy;
            
            if (_hasRigidBody)
            {
                _rigidbody.freezeRotation = true;
                _rigidbody.useGravity = false;
                _rigidbody.detectCollisions = false;
            }
            
            UpdatePosition(true);
        }

        public void Release()
        {
            _heldByCharacter.CharInteractor.HeldInteractable = null;
            _heldByCharacter = null;
            _heldByTransform = null;
            _holdingChar = null;
            
            // reparent to original
            _transform.SetParent(_defaultParentTransform, true);
            
            if (_hasRigidBody)
            {
                _rigidbody.freezeRotation = false;
                _rigidbody.useGravity = true;
                _rigidbody.detectCollisions = true;
            }
        }

        public void UpdatePosition(bool firstUpdate=false)
        {
            if (_holdingChar == CharType.Boy)
            {
                UpdatePositionWithOffsets(boyHoldingOffset, _boyHoldingQuaternion, firstUpdate);
            }
            else // _holdingChar == CharType.Dog
            {
                UpdatePositionWithOffsets(dogHoldingOffset, _dogHoldingQuaternion, firstUpdate);
            }
        }
        
        private void UpdatePositionWithOffsets(Vector3 positionOffset, Quaternion rotationOffset, bool firstUpdate)
        {
            var updatedPosition = _heldByTransform.position + _heldByTransform.TransformVector(positionOffset);
            var updatedRotation = _heldByTransform.rotation * rotationOffset;
            
            if (!firstUpdate && _hasRigidBody)
            {
                _rigidbody.MovePosition(updatedPosition);
                _rigidbody.MoveRotation(updatedRotation);
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
            else
            {
                _transform.position = updatedPosition;
                _transform.rotation = updatedRotation;
            }
        }
    }
}