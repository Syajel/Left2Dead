using System;
using System.Collections;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    int wepIndex = 0;
    [HideInInspector] public GameObject weap;
    public ArrayList availableWeapons;
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        availableWeapons = new ArrayList();
        availableWeapons.Add(0);
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
          
            if (wepIndex == availableWeapons.Count-1)
                wepIndex = 0;
            else
                wepIndex++;
            selectedWeapon = (int)availableWeapons[wepIndex];
            FindObjectOfType<AudioManager>().play("switchweapon");
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon){
                weapon.gameObject.SetActive(true);
                weap = weapon.gameObject;
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void addWeapon(int x)
    {
        if (availableWeapons.Contains(x))
        {
            return;
        }
        else
        {
            availableWeapons.Add(x);
            availableWeapons.Sort();
        }
    }

    public void addAmmo()
    {
        foreach (Transform weapon in transform)
        {
            weapon.GetComponent<Gun>().AddAmmo();
        }
    }
}
