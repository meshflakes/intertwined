using UnityEngine;

public class FallingPipe : MonoBehaviour
{
    private Rigidbody selfRigid;
    private MeshCollider selfMesh;
    public GameObject dynamicPipeCollider;

    private bool _moved = false;
    
    void Start()
    {
        selfRigid = GetComponent<Rigidbody>();
        selfMesh = GetComponent<MeshCollider>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (_moved) return;
        
        if (other.name == "Dog Collider")
        {
            selfRigid.velocity = new Vector3(0, 2f, 5);
            selfMesh.convex = true;
            selfRigid.isKinematic = false;
            selfRigid.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | 
                                    RigidbodyConstraints.FreezePositionX;
            dynamicPipeCollider.SetActive(true);

            Invoke(nameof(DisablePipeMotion), 2);
        }
    }

    protected void DisablePipeMotion()
    {
        _moved = true;
        selfRigid.constraints = RigidbodyConstraints.FreezeAll;
    }
}
