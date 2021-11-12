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
    public float maxDist = 2;
    public float _followTimeDelta = 0.8f;
    public float smoothness = 0.5f;
    
    private Vector3 _cameraOffset;
    private Vector3 newPos;

    public Camera cam;
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
            rotateCam(0.2f);
        } else if (Input.GetMouseButton(1))
        {
            rotateCam(-0.2f);
        }
        transform.LookAt((p1.position + p2.position) / 2f);

        Vector3 midpoint = (p1.position + p2.position) / 2f;
        float distance = (p1.position - p2.position).magnitude;
        if (distance > maxDist)
        {
            distance = maxDist;
            
            //This is when the camera stops expanding. This is the time to check if characters are moving out of bounds.
            //TODO: Prevent characters from moving out of bounds
            Vector3 vp1 = cam.WorldToViewportPoint(p1.position);
            Vector3 vp2 = cam.WorldToViewportPoint(p2.position);
            if (!(vp1.z > 0 && vp1.x > 0 && vp1.x < 1 && vp1.y > 0 && vp1.y < 1))
            {
                Debug.Log("Player 1 out of range");
            }
            if (!(vp2.z > 0 && vp2.x > 0 && vp2.x < 1 && vp2.y > 0 && vp2.y < 1))
            {
                Debug.Log("Player 2 out of range");
            }
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

    public void rotateCam(float angle)
    {
        _cameraOffset = Quaternion.AngleAxis(angle, Vector3.up) * _cameraOffset;
    }
}
