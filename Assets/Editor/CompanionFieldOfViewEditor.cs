using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (CompanionFieldOfView))]
public class CompanionFieldOfViewEditor : Editor {

	void OnSceneGUI() {
		CompanionFieldOfView fow = (CompanionFieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc (fow.transform.position+fow.lineOfSight, Vector3.up, Vector3.forward, 360, fow.viewRadius);
		Vector3 viewAngleA = fow.DirFromAngle (-fow.viewAngle / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle (fow.viewAngle / 2, false);

		Handles.DrawLine (fow.transform.position+fow.lineOfSight, fow.transform.position+fow.lineOfSight + viewAngleA * fow.viewRadius);
		Handles.DrawLine (fow.transform.position+fow.lineOfSight, fow.transform.position+fow.lineOfSight + viewAngleB * fow.viewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in fow.visibleTargets) {
			Handles.DrawLine (fow.transform.position, visibleTarget.position);
		}
	} 

}