using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopDamage : MonoBehaviour
{
    private float time1 = 1f;
    private float time5 = 5f;
    [HideInInspector]
    public bool confused = false;
    private JoelMeters joelMeter;
    private joelAnimator joelAnimator;
    // Start is called before the first frame update
    void Start()
    {
        joelAnimator = PlayerManager.instance.player.GetComponentInChildren<joelAnimator>();
        joelMeter = PlayerManager.instance.player.GetComponent<JoelMeters>();
    }

    // Update is called once per frame
    void Update()
    {
        time1 -= Time.deltaTime;
        time5 -= Time.deltaTime;
        if(time5 <= 0)
            Destroy(this.gameObject);
    }
    private void OnTriggerStay(Collider other){
        // Deal 20 damage/second to player
        if(time1 <= 0f){
            if(confused){
                // Damage infected
                if(other.GetComponent<InfectedController>() != null){
                    // Target is transform!!
                    other.GetComponent<InfectedController>().TakeDamage(20);
                }
            }
            else{
                joelMeter.takeDamage(20);
                joelAnimator.injured();
            }
            time1 = 1f;
        }
    }
}
