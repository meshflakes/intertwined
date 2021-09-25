using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTether : MonoBehaviour
{
    private LineRenderer lr;
    private float counter;
    private float dist;
    
    public bool tooFar = false;
    
    public Transform p1;
    public Transform p2;

    public float drawSpeed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, p1.position);
        lr.SetWidth(.15f, .15f);
        
        dist = Vector3.Distance(p1.position, p2.position);
    }

    // Update is called once per frame
    void Update()
    {
        counter += .1f/drawSpeed;
        Vector3 pointA = p1.position;
        Vector3 pointB = p2.position;
        dist = Vector3.Distance(pointA, pointB);
        float x = Mathf.Lerp(0, dist, counter);
        Vector3 pointAlongLine = x * Vector3.Normalize(pointB-pointA) + pointA;
        lr.SetPosition(0, pointA);
        lr.SetPosition(1, pointAlongLine);

        if (dist >= 3.5)
        {
            tooFar = true;
        }
        else
        {
            tooFar = false;
        }

    }
}
