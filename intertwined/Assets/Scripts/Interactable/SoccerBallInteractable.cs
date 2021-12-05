using UnityEngine;

namespace Interactable
{
    [RequireComponent(typeof(Rigidbody))]
    public class SoccerBallInteractable : Interactable
    {
        private Rigidbody _rigidbody;
        [SerializeField] private float kickHorizontalVelocity = 3;
        [SerializeField] private float kickVerticalVelocity = 3;
        
        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override bool Interact(Character.Character interacter)
        {
            var rotation = interacter.transform.rotation;

            _rigidbody.velocity += kickHorizontalVelocity * (rotation * Vector3.forward);
            _rigidbody.velocity += kickVerticalVelocity * Vector3.up;
            // _rigidbody.AddForce(kickForce * (rotation * Vector3.forward));
            return true;
        }

        public override bool UsedWith(Interactable other)
        {
            return false; 
        }
    }
}