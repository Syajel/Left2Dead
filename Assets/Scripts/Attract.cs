using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{

    private InfectedController ic;
    
    //Make loud beeps

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<InfectedController>() != null)
        {
            ic = other.GetComponent<InfectedController>();
            ic.Attract(transform);
        }
    }
}
