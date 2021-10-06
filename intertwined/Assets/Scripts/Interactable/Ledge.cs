using UnityEngine;

namespace Interactable
{
    public class Ledge : Interactable
    {
        [Tooltip("The position where the boy will end after the climb")]
        public Vector3 finalPos;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Boy")) return;
        
            var character = other.gameObject.GetComponent<Character.Character>();
            character.interactable = this;
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Boy")) return;
        
            var character = other.gameObject.GetComponent<Character.Character>();
            character.interactable = null;
        }

        public override void Interact(Character.Character interacter)
        {
            interacter.transform.position = finalPos;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.35f);
            Gizmos.DrawWireCube(finalPos, new Vector3(0.5f, 0.1f, 0.5f));
        }
    }
}
