using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confuse : MonoBehaviour
{

    private InfectedController ic;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(confuseCoroutine(other));
    }

    IEnumerator confuseCoroutine(Collider other){
        if (other.GetComponent<InfectedController>() != null)
        {
            ic = other.GetComponent<InfectedController>();
            
            ic.fieldOfView.confused = true;
            ic.confused = true;
            if(ic.fieldOfView.visibleTargets.Count>0)
                ic.target = ic.fieldOfView.visibleTargets[0];
            yield return new WaitForSeconds(3f);
            ic.confused = false;
            ic.fieldOfView.confused = false;
            ic.target = PlayerManager.instance.player.transform;
        }
    }
}
