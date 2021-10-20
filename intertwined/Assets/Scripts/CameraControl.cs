using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Collider camTrigger;

    public float zoom = 0.165f;
    public float zoomStartDist = 6;
    public float maxDist = 12;
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
        if (Input.GetMouseButton(0))
        {
            rotateCam();
        }
        transform.LookAt((p1.position + p2.position) / 2f);

        Vector3 midpoint = (p1.position + p2.position) / 2f;
        float distance = (p1.position - p2.position).magnitude;
        if (distance > maxDist)
        {
            distance = maxDist;
        }
        
        
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

    public void rotateCam()
    {
        _cameraOffset = Quaternion.AngleAxis(0.2f, Vector3.up) * _cameraOffset;
    }
}
