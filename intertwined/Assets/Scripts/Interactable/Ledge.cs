using UnityEngine;

namespace Interactable
{
    public class Ledge : Interactable
    {
        [Tooltip("The position where the boy will end after the climb")]
        public Vector3 finalPos;

        public override bool Interact(Character.Character interacter)
        {
            interacter.transform.position = finalPos;
            return true;
        }

        public override bool UsedWith(Interactable other)
        {
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.35f);
            Gizmos.DrawWireCube(finalPos, new Vector3(0.5f, 0.1f, 0.5f));
        }
    }
}
