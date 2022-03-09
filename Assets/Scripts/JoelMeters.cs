using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoelMeters : MonoBehaviour
{
    [HideInInspector] public static int maxHp = 300;
    private int maxRageMeter = 100;
    private float time3 = 3f;
    [HideInInspector] public static int curHp = maxHp;
    private int curRageMeter;
    private int damageTaken;
    private InfectedController infectedController;
    public bool isRageMode = false;

    CompanionManager cmp;
    bool isChecked = false;
    void Start()
    {
        curHp = maxHp;
        curRageMeter = maxRageMeter;
        infectedController = this.GetComponent<InfectedController>();
        cmp = GameObject.Find("CompanionManager").GetComponent<CompanionManager>();
        
        
    }

    void Update()
    {
        rageModeOn();
        time3 -= Time.deltaTime;
        if (time3 <= 0)
            resetRageLimit();
        if (curHp == 0)
        {
            FindObjectOfType<AudioManager>().play("dying");
        }
    }
    IEnumerator HealthAbility() {
        for(;;) {
            curHp++;
            yield return new WaitForSeconds(.1f);
        }
    }
 
    public void takeDamage(int damageTaken)
    {
        if(!isChecked){
            isChecked = true;
            if(cmp.instance.name.Equals("Louis") || cmp.instance.name.Equals("Louis(Clone)")){
                StartCoroutine("HealthAbility");
        }
        }
        if(curHp>0)
        {
            curHp -= damageTaken;
            FindObjectOfType<AudioManager>().play("hit");
        }
    }
    public void gainHPFromHealthPack()
    {
        curHp += 50;
        if(curHp > maxHp)
        {
            curHp = maxHp;
        }
    }
    public void increaseRage()
    {
        if(infectedController.type == "normal")
        {
            curRageMeter += 10;
        }else
        {
            curRageMeter += 50;
        }
        if(cmp.instance.name.Equals("Ellie(Clone)") || cmp.instance.name.Equals("Ellie")){
            curRageMeter *= 2;
        }
    }
    public void resetRageLimit()
    {
        time3 = 3f;
    }
    public void rageModeOn()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (curRageMeter >= maxRageMeter)
            {
                isRageMode = true;
                FindObjectOfType<AudioManager>().play("ragemode");
                StartCoroutine(CurrentlyRaging(7f));
                isRageMode = false;
            }
        }
    }
    private IEnumerator CurrentlyRaging(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    public bool isRageModeOn()
    {
        return isRageMode;
    }

}
