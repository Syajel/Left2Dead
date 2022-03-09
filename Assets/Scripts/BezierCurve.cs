using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    Vector3 p1,p0,p2;
    [HideInInspector]
    public Vector3[] positions = new Vector3[50];
    private int numPoints = 50;
    private FieldOfView fieldOfView;
    private LineRenderer lineRenderer;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Vector3 spawn;
    

    public Vector3 CalculateBezierPoint(float t){
        float u = 1-t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    public void DrawBezierCurve(){
        fieldOfView = GetComponent<FieldOfView>();
        
        if(GetComponent<LineRenderer>() != null){
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = numPoints;
        }
    
        p0 = new Vector3(spawn.x,
                        spawn.y,
                        spawn.z);

        p2 = target.position;
        // Change to fall on the terrain later
        p2.y = 0f;
        
        float x1 = (p0.x + p2.x) / 2f;
        float z1 = (p0.z + p2.z) / 2f;
        p1 = new Vector3(x1, 6f, z1);

        Debug.Log("Bezier Draw()");
        for(int i = 1; i<numPoints+1; i++){
            float t = i / (float)numPoints;
            positions[i-1] = CalculateBezierPoint(t);
        }
        if(lineRenderer != null){
            lineRenderer.SetPositions(positions);
        }
    }
}