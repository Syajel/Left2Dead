using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAnimation : MonoBehaviour
{
    CompanionController companion;
    Animator animator;
    bool isWalking = false;
    bool isRunning = false;
    bool isShooting = false;

    GameObject weapon;
    Transform graspWeapon;
    Transform releaseWeapon;

    void Start(){
        animator = GetComponent<Animator>();
        companion = GetComponent<CompanionController>();

        weapon = Instantiate(companion.companion.weapon);
        graspWeapon = GameObject.Find(companion.companion.graspWeaponName).GetComponent<Transform>();
        releaseWeapon = GameObject.Find(companion.companion.releaseWeaponName).GetComponent<Transform>();
        Idle();
    }

    public void Walk(){
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Walking")){
            isShooting = false;
            isWalking = true;
            isRunning = false;
            PlayAnim();
        }        
    }

    public void Run(){
        isShooting = false;
        isWalking = true;
        isRunning = true;
        PlayAnim();
    }

    public void Shoot(bool action){
        isShooting = action;   
        PlayAnim();             
    }

    public void Stop(){
        isWalking = false;
        isRunning = false;
        isShooting = false;
        PlayAnim();
    }

    public void Idle(){
        isWalking = false;
        isRunning = false;
        Shoot(false);
        PlayAnim();
    }

    void PlayAnim(){
        if(isShooting)
            WeaponPositionShoot();
        else WeaponPositionNormal();

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isShooting", isShooting);
    }

    void WeaponPositionShoot(){
        weapon.transform.SetParent(graspWeapon);
        weapon.transform.localPosition = companion.companion.weaponShootingPosition;
        weapon.transform.localEulerAngles = companion.companion.weaponShootingEulerAngle;
        weapon.transform.localScale = companion.companion.weaponShootingScale;
    }

    void WeaponPositionNormal(){
        weapon.transform.SetParent(releaseWeapon);
        weapon.transform.localPosition = companion.companion.weaponNormalPosition;
        weapon.transform.localEulerAngles = companion.companion.weaponNormalEulerAngle;
        weapon.transform.localScale = companion.companion.weaponNormalScale;
    }
    
}
