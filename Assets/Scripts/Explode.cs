using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public float delay = 3f;
    bool hasExploded = false;
    public GameObject explosionEffect;

    float countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Boom();
            hasExploded = true;
        }
    }

    void Boom()
    {
        GameObject explosionGO = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(explosionGO, 5f);
        Destroy(gameObject);
    }
}
