using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Companion", menuName = "Companion")]
public class Companion : ScriptableObject
{
    
    public new string name;
    public string backgroundInfo;
    public GameObject weapon;
    public int damage;
    public int clipNumber;
    public int ClipCapacity;
    public float rateOfFire;
    public string graspWeaponName;
    public Vector3 weaponShootingPosition;
    public Vector3 weaponShootingEulerAngle;
    public Vector3 weaponShootingScale;

    //releaseWeaponName is the body part where the weapon would be attached
    public string releaseWeaponName;
    public Vector3 weaponNormalPosition;
    public Vector3 weaponNormalEulerAngle;
    public Vector3 weaponNormalScale;

}
