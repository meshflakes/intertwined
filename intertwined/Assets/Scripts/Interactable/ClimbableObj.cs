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

        private void Start()
        {
            if (defaultClimbDirection == Vector3.zero)
            {
                throw new ArgumentException("defaultClimbDirection attribute must be non-zero");
            }
            defaultClimbDirQuaternion = Quaternion.Euler(defaultClimbDirection);
        }
        
        // private void OnCollisionEnter(Collision other)
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision Detected");
            if (CollidingObjectCanInteract(other) && CurrentlyClimbable())
            {
                // TODO: check angle of approach (glance vs full collision)
                // TODO: calculate starting position
                other.gameObject.GetComponent<Character.Character>()
                    .StartClimbing(
                        this, 
                        Vector3.zero,
                        GetClimbingDirection(other.transform.position));
                Debug.Log("setting as in-focus movement interactable");
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (CollidingObjectCanInteract(other))
            {
                other.gameObject.GetComponent<Character.Character>()
                    .StopClimbing();
                Debug.Log("removing as in-focus movement interactable");
            }
        }
        
        private bool CollidingObjectCanInteract(Collider other)
        {
            Debug.Log(other.CompareTag("Boy"));
            return (boyCanInteract && other.CompareTag("Boy"))
                   || (dogCanInteract && other.CompareTag("Dog"));
        }

        public bool CurrentlyClimbable()
        {
            // TODO: add angle etc. checks here
            return !ClimbingDisabled;
        }
        
        public Quaternion GetClimbingDirection(Vector3 currentCharPosition)
        {
            // TODO: adjust for rotation of object
            // return gameObject.transform.rotation;
            return  defaultClimbDirQuaternion * transform.rotation;
        }
    }
}