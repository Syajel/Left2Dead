using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCurve : MonoBehaviour
{
    Transform p0,p2;
    Vector3 p1,p0New;
    [HideInInspector]
    public Vector3[] positions = new Vector3[50];
    private int numPoints = 50;
    private FieldOfView fieldOfView;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        p0 = transform;
        p2 = PlayerManager.instance.player.transform;
        
        if(GetComponent<LineRenderer>() != null){
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = numPoints;
        }
    }
    private void Update() { 
        p0New = new Vector3(p0.transform.position.x,
                            fieldOfView.lineOfSightFactor,
                            p0.transform.position.z);
        float x1 = (p0.position.x + p2.position.x) / 2f;
        float z1 = (p0.position.z + p2.position.z) / 2f;
        p1 = new Vector3(x1, p2.position.y+4f, z1);
    }

    public Vector3 CalculateBezierPoint(float t){
        float u = 1-t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0New;
        p += 2 * u * t * p1;
        p += tt * p2.position;
        return p;
    }

    public void DrawBezierCurve(){
        for(int i = 1; i<numPoints+1; i++){
            float t = i / (float)numPoints;
            positions[i-1] = CalculateBezierPoint(t);
        }
        if(lineRenderer != null){
            lineRenderer.SetPositions(positions);
        }
    }
}