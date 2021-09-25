using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    public float zoom = 0.1f;
    public float zoomStartDist = 10;
    public float _followTimeDelta = 0.8f;
    public float smoothness = 0.5f;
    
    private Vector3 _cameraOffset;
    private Vector3 newPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - ((p1.position + p2.position) / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midpoint = (p1.position + p2.position) / 2f;
        float distance = (p1.position - p2.position).magnitude;
        if (distance > zoomStartDist)
        {
            newPos = midpoint + _cameraOffset * distance * zoom;
        }
        else
        {
            newPos = midpoint + _cameraOffset;
        }
        

        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);

    }
}
