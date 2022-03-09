using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CompanionController : MonoBehaviour
{
    //Companion
    public Companion companion;
    NavMeshAgent companionAgent;
    CompanionFieldOfView fieldOfView;
    CompanionAnimation anim;
    int acceleration = 1;
    float speed = 2f;
    float stoppingDistance = 5f;

    GameObject weapon;

    public int companionAmmo;
    public int companionClip;
    float companionNextTimeToFire;
    //Player
    Camera player;

    //Enemy
    GameObject targetToShoot;
    bool canAttack = true;


    void Start()
    {
        fieldOfView = GetComponent<CompanionFieldOfView>();
        
        companionAgent = GetComponent<NavMeshAgent>();
        companionAgent.speed = speed;
        companionAgent.acceleration = acceleration;
        companionAgent.stoppingDistance = stoppingDistance;
        anim = GetComponent<CompanionAnimation>();

        companionAmmo = companion.ClipCapacity;
        companionClip = 1;
        //player = GameObject.Find("PlayerController").GetComponent<PlayerManager>();
        player = Camera.main;
        MoveToPoint(player.transform.position);
       

    }


    void Update()
    {
        if (Input.GetKeyDown("q"))
            canAttack = !canAttack;
        if(Time.frameCount % 5 == 0){
            float dist = Vector3.Distance(player.transform.position, companionAgent.transform.position);
            //If distance between companion and player is more than the accepted range
            if(dist > stoppingDistance && targetToShoot == null || dist > stoppingDistance * 2f ){
                //move companion to player
                targetToShoot = null;
                MoveToPoint(player.transform.position);
                if(dist > stoppingDistance * 1.5f){
                    anim.Run();
                    companionAgent.speed = speed * 2.5f;
                }else {
                    companionAgent.speed = speed;
                    anim.Walk();
                }

            }else{
                //If distance is in the accepted range

                //If detected an infected
                if(fieldOfView.detected)
                {
                    //search for special infected
                    foreach (var target in fieldOfView.visibleTargets)
                    {
                        if(target.transform == null)
                            fieldOfView.visibleTargets.Remove(target);
                        else if(target.CompareTag("SpecialInfected")){
                            fieldOfView.visibleTargets.Insert(0, target);
                            break;
                        }
                    }
                    //attack
                    //check here target's health, if dead increment the index in visibleTargets
                    targetToShoot = fieldOfView.visibleTargets[0].gameObject;
                    companionAgent.transform.LookAt(targetToShoot.transform.position);
                    companionAgent.velocity = Vector3.zero;
                    if(companionAmmo > 0 && Time.time >= companionNextTimeToFire && canAttack){
                        anim.Shoot(true);
                        CompanionShoot();
                        companionNextTimeToFire = Time.time + 1f/companion.rateOfFire;
                        targetToShoot.GetComponent<InfectedController>().TakeDamage(companion.damage);
                    }
                    if(targetToShoot.transform == null)
                        if(fieldOfView.visibleTargets.Count > 0)
                            fieldOfView.visibleTargets.RemoveAt(0);
                }
                else{
                    companionAgent.velocity = Vector3.zero;
                    anim.Idle();
                }
            }

            if(InfectedController.incraseCompanionClip){
                if(companionClip < companion.clipNumber)
                    companionClip++;
                InfectedController.incraseCompanionClip = false;
            }
        }
    }

    public void MoveToPoint(Vector3 point){
        companionAgent.SetDestination(point);
    }       

    void CompanionShoot(){
        companionAmmo--;
        if(companionAmmo == 0 && companionClip > 0){
            companionClip--;
            companionAmmo = companion.ClipCapacity;
        }
    }
}