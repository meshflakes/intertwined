using UnityEngine;

namespace Character
{
    /**
     * Disable collisions between the character's collider & its character collision blocker collider
     *
     * reference: https://www.youtube.com/watch?v=-yjKyI8NfKA
     */
    public class BlockCharacterCollision : MonoBehaviour
    {
        public Collider characterCollider;
        public Collider characterCollisionBlocker;
        
        protected void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollisionBlocker, true);
        }
    }
}