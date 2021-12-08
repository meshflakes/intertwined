using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPipe : MonoBehaviour
{
    private Rigidbody selfRigid;
    private MeshCollider selfMesh;
    // Start is called before the first frame update
    void Start()
    {
        selfRigid = GetComponent<Rigidbody>();
        selfMesh = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        else
        {
            Debug.Log(other.name);
        }
    }
}
