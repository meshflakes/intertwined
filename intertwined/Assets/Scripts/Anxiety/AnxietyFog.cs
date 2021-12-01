using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyFog : MonoBehaviour
{
    public GameObject fogPlane;
    public Transform boy;
    public LayerMask fogLayer;
    public float radius = 5f;
    private float radiusSqred { get { return radius*radius; }}
	
    private Mesh mesh;
    private Vector3[] planeVertices;
    private Color[] planeColors;
	
    // Use this for initialization
    void Start () {
        Initialize();
    }
	
    // Update is called once per frame
    void Update () {
        Ray r = new Ray(transform.position, boy.position - transform.position);
        RaycastHit hit;
        Debug.Log(boy.position);
        if (Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide))
        {
            Debug.Log("hit");
            for (int i=0; i< planeVertices.Length; i++) {
                Vector3 v = fogPlane.transform.TransformPoint(planeVertices[i]);
                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < radiusSqred) {
                    Debug.Log("set");
                    float alpha = Mathf.Min(planeColors[i].a, dist/radiusSqred);
                    planeColors[i].a = alpha;
                }
            }
            UpdateColor();
        }
    }
	
    void Initialize() {
        mesh = fogPlane.GetComponent<MeshFilter>().mesh;
        planeVertices = mesh.vertices;
        planeColors = new Color[planeVertices.Length];
        for (int i=0; i < planeColors.Length; i++) {
            planeColors[i] = Color.black;
            Debug.Log("black");
        }
        UpdateColor();
    }
	
    void UpdateColor() {
        mesh.colors = planeColors;
    }
}

