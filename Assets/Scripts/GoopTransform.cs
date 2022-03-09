using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopTransform : MonoBehaviour
{
    BezierCurve bezierCurve;
    public GameObject goopPool;
    [HideInInspector]
    public bool confused;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Vector3 infectedTransform;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(){
        bezierCurve = GetComponent<BezierCurve>();
        bezierCurve.target = target;
        bezierCurve.spawn = infectedTransform; 
        transform.position = infectedTransform; 
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine(){
        bezierCurve.DrawBezierCurve();
        foreach (Vector3 pos in bezierCurve.positions){
            yield return new WaitForSeconds(0.02f);
            transform.position = pos;
        }
        
        Vector3 finalPos = bezierCurve.positions[bezierCurve.positions.Length-1];
        GameObject tempGoopPuddle = Instantiate(goopPool, finalPos, Quaternion.identity);
        tempGoopPuddle.GetComponent<GoopDamage>().confused = confused;
        Destroy(this.gameObject);
    }

}
