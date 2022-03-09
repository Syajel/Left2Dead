using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompanionFieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector]
	public bool detected;
	[HideInInspector]
	public Vector3 lineOfSight;
	[Range(0.1f,2f)]
	public float lineOfSightFactor;

	void Start() {
		StartCoroutine("FindTargetsWithDelay", .2f);
	}

	private void Update() {
		lineOfSight = transform.up*lineOfSightFactor;
	}

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds(delay);
			detected = false;
			FindVisibleTargets();
		}
	}

	void FindVisibleTargets() {
        visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        // Player in range of character's sight.
		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			// Player in biew angle.
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance(transform.position, target.position);
                // No obstacles between infected and player.
				if (!Physics.Raycast(transform.position+lineOfSight, dirToTarget, dstToTarget, obstacleMask)) {
					detected = true;
                    visibleTargets.Add(target);
				}
			}
		}
	}

    // Takes an angle and returns the direction of that angle.
	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

    
}