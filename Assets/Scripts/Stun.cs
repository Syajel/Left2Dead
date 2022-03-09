using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{

    private InfectedController ic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InfectedController>() != null)
        {
            ic = other.GetComponent<InfectedController>();
            ic.Stun();
        }
    }
}
