using UnityEngine;

namespace Interactable.ConstructionSite
{
    public class PipeInteractable : Interactable
    {
        [Tooltip("Collider that blocks the connection to the pipe after the pipe is gone")]
        public GameObject dynamicPipeCollider;

        private Rigidbody _rigidbody;
        private MeshCollider _meshCollider;
        private bool _moved = false;

        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshCollider = GetComponent<MeshCollider>();
        }

        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            if (!enteredTrigger || _moved) return;
            
            _rigidbody.velocity = new Vector3(0, 2f, 5);
            _meshCollider.convex = true;
            _rigidbody.isKinematic = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY |
                                    RigidbodyConstraints.FreezePositionX;
            dynamicPipeCollider.SetActive(true);
            
            Invoke(nameof(DisablePipeMotion), 2);
        }

        protected void DisablePipeMotion()
        {
            _moved = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
