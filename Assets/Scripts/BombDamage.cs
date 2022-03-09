using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{

    private InfectedController ic;
    private int damage = 100;

    private void onTriggerEnter(Collider other)
    {
        if (other.GetComponent<InfectedController>() != null)
        {
            ic = other.GetComponent<InfectedController>();
            ic.TakeDamage(damage);
        }
    }
}
