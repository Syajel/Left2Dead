using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float distance ;
    public Transform equipPosition;
    GameObject currentObject;
    WeaponSwitching weaponSwitching;
    GrenadeSwitching grenadeSwitching;
    CompanionManager cmp;
    

    bool canGrab;

    void Start()
    {
        weaponSwitching = transform.GetComponentInChildren<WeaponSwitching>();
        grenadeSwitching = transform.GetComponentInChildren<GrenadeSwitching>();
        cmp = GameObject.Find("CompanionManager").GetComponent<CompanionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        checkGrabable();
        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().play("pickup");
                PickUp();
            }
        }
    }

    void checkGrabable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {

            if (hit.transform.tag == "isGrabable")
            {
                currentObject = hit.transform.gameObject;
                canGrab = true;
            }
            else
                canGrab = false;
        }
    }

    private void PickUp()
    {
        int amount = 1;
        if(cmp.instance.name.Equals("Zoey") || cmp.instance.name.Equals("Zoey(Clone)"))
            amount = 2;
        switch (currentObject.name)
        {
            case "rifle_2":
                weaponSwitching.addWeapon(1);
                Destroy(currentObject);
                break;

            case "shotgun_1":
                weaponSwitching.addWeapon(2);
                Destroy(currentObject);
                break;

            case "submachine_gun_5":
                weaponSwitching.addWeapon(3);
                Destroy(currentObject);
                break;

            case "machine_gun_1":
                weaponSwitching.addWeapon(4);
                Destroy(currentObject);
                break;

            case "explosive_4":
                Inventory.instance.AddItem("Molotov Cocktail", 1);
                grenadeSwitching.addGrenade(0);
                grenadeSwitching.addAmmo(0);
                Destroy(currentObject);
                break;

            case "explosive_1":
                Inventory.instance.AddItem("Pipe Bomb", 1);
                grenadeSwitching.addGrenade(1);
                grenadeSwitching.addAmmo(1);
                Destroy(currentObject);
                break;

            case "explosive_2":
                Inventory.instance.AddItem("Bile Bomb", 1);
                grenadeSwitching.addGrenade(2);
                grenadeSwitching.addAmmo(2);
                Destroy(currentObject);
                break;

            case "explosive_3":
                Inventory.instance.AddItem("Stun Grenade", 1);
                grenadeSwitching.addGrenade(3);
                grenadeSwitching.addAmmo(3);
                Destroy(currentObject);
                break;

            case "Ammunition":
                weaponSwitching.addAmmo();
                Destroy(currentObject);
                break;

            case "Gunpowder":
                Inventory.instance.AddSupply("gunpowder", amount);
                Destroy(currentObject);
                break;

            case "Sugar":
                Inventory.instance.AddSupply("sugar", amount); 
                Destroy(currentObject);
                break;

            case "Alcohol":
                Inventory.instance.AddSupply("alcohol", amount);
                Destroy(currentObject);
                break;

            case "Canister":
                Inventory.instance.AddSupply("canisters", amount);
                Destroy(currentObject);
                break;

            case "Rag":
                Inventory.instance.AddSupply("rags", amount);
                Destroy(currentObject);
                break;

            default:
                break;
        }
    }
}
