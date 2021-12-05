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

        protected bool HasRigidBody;
        protected Rigidbody GrabbableRigidbody;
        protected Transform GrabbableTransform;
        private Transform _defaultParentTransform;
        
        protected void Start()
        {
            GrabbableRigidbody = gameObject.GetComponent<Rigidbody>();
            HasRigidBody = GrabbableRigidbody != null;
            
            GrabbableTransform = gameObject.GetComponent<Transform>();
            _defaultParentTransform = GrabbableTransform.parent;
            
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
            GrabbableTransform.SetParent(_heldByTransform, true);
            _heldByTransform = obj.GetComponent<Transform>();
            
            _heldByCharacter = obj.GetComponentInParent<Character.Character>();
            _heldByCharacter.CharInteractor.HeldInteractable = this;
            _holdingChar = obj.CompareTag("Dog") ? CharType.Dog : CharType.Boy;
            
            if (HasRigidBody)
            {
                GrabbableRigidbody.freezeRotation = true;
                GrabbableRigidbody.useGravity = false;
                GrabbableRigidbody.detectCollisions = false;
            }
            
            UpdatePosition(true);
        }

        public virtual void Release()
        {
            _heldByCharacter.CharInteractor.HeldInteractable = null;
            _heldByCharacter = null;
            _heldByTransform = null;
            _holdingChar = null;
            
            // reparent to original
            GrabbableTransform.SetParent(_defaultParentTransform, true);
            
            if (HasRigidBody)
            {
                GrabbableRigidbody.freezeRotation = false;
                GrabbableRigidbody.useGravity = true;
                GrabbableRigidbody.detectCollisions = true;
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
            
            if (!firstUpdate && HasRigidBody)
            {
                GrabbableRigidbody.MovePosition(updatedPosition);
                GrabbableRigidbody.MoveRotation(updatedRotation);
                GrabbableRigidbody.velocity = Vector3.zero;
                GrabbableRigidbody.angularVelocity = Vector3.zero;
            }
            else
            {
                GrabbableTransform.position = updatedPosition;
                GrabbableTransform.rotation = updatedRotation;
            }
        }
    }
}