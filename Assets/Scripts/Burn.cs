using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    private int damage = 25;
    private float delay = 0f;
    private InfectedController ic;

    // Update is called once per frame
    void Update()
    {
        if(delay > 0)
        {
            delay -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(delay <= 0)
        {
            if (other.GetComponent<InfectedController>() != null)
            {
                ic = other.GetComponent<InfectedController>();
                ic.TakeDamage(damage);
                delay = 1f;
            }
        }
    }
}
