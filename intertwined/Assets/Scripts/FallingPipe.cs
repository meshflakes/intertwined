using UnityEngine;

public class FallingPipe : MonoBehaviour
{
    private Rigidbody selfRigid;
    private MeshCollider selfMesh;
    public GameObject dynamicPipeCollider;
    
    void Start()
    {
        selfRigid = GetComponent<Rigidbody>();
        selfMesh = GetComponent<MeshCollider>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.name == "Dog Collider")
        {
            transform.position = transform.position + new Vector3(0, 0.5f, 3);
            selfMesh.convex = true;
            selfRigid.isKinematic = false;
            selfRigid.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | 
                                    RigidbodyConstraints.FreezePositionX;
            dynamicPipeCollider.SetActive(true);
        }
    }
}
