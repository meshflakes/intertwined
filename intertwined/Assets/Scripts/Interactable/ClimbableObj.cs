using System;
using UnityEngine;

namespace Interactable
{
    public class ClimbableObj : MonoBehaviour
    {
        [Tooltip("Allows the dog to interact with this Interactable")]
        public bool dogCanInteract;

        [Tooltip("Allows the boy to interact with this Interactable")]
        public bool boyCanInteract = true;

        [Tooltip("Climbing direction relative to object's default position (rotation=0); must be non-zero")]
        public Vector3 defaultClimbDirection = new Vector3(0, 1, 0);

        private Quaternion defaultClimbDirQuaternion;
        
        [NonSerialized]
        public bool ClimbingDisabled = false;

        protected bool BeingClimbed => _numCharsClimbing > 0;
        private int _numCharsClimbing = 0;
        private const int MaxClimbableAngle = 42;

        protected void Start()
        {
            if (defaultClimbDirection == Vector3.zero)
            {
                throw new ArgumentException("defaultClimbDirection attribute must be non-zero");
            }
            defaultClimbDirQuaternion = Quaternion.Euler(defaultClimbDirection);
        }
        
        protected void OnTriggerEnter(Collider other)
        {
            Debug.Log($"CollidingObjectCanInteract(other)={CollidingObjectCanInteract(other)}, CurrentlyClimbable()={CurrentlyClimbable()}");
            if (CollidingObjectCanInteract(other) && CurrentlyClimbable())
            {
                // TODO: check angle of approach (glance vs full collision)
                // TODO: calculate starting position
                other.gameObject.GetComponent<Character.Character>()
                    .StartClimbing(
                        this, 
                        Vector3.zero,
                        GetClimbingDirection(other.transform.position));
                _numCharsClimbing++;
                Debug.Log($"setting {gameObject.name} as in-focus movement interactable");
            }
        }
    
        protected void OnTriggerExit(Collider other)
        {
            if (CollidingObjectCanInteract(other))
            {
                other.gameObject.GetComponent<Character.Character>()
                    .StopClimbing();
                _numCharsClimbing--;
                Debug.Log($"removing {gameObject.name} as in-focus movement interactable");
            }
        }
        
        protected bool CollidingObjectCanInteract(Collider other)
        {
            return (boyCanInteract && other.CompareTag("Boy"))
                   || (dogCanInteract && other.CompareTag("Dog"));
        }

        public bool CurrentlyClimbable()
        {
            if (ClimbingDisabled) return false;

            var eulerRotation = transform.rotation.eulerAngles;
            if (eulerRotation.x > 180) eulerRotation.x -= 360;
            if (eulerRotation.z > 180) eulerRotation.z -= 360;
            
            var xzRotationMagnitude = Math.Sqrt(Math.Pow(eulerRotation.x, 2) + Math.Pow(eulerRotation.z, 2));
            return xzRotationMagnitude <= MaxClimbableAngle;
        }
        
        public Quaternion GetClimbingDirection(Vector3 currentCharPosition)
        {
            // TODO: use defaultClimbDir to affect climbing direction
            // return  defaultClimbDirQuaternion * transform.rotation;
            return transform.rotation;
        }
    }
}