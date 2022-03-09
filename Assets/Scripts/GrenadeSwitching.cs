using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeSwitching : MonoBehaviour
{
    int grenIndex = 0;
    [HideInInspector] public GameObject gren;
    public ArrayList availableGrenades;
    public int selectedGrenades = 0;

    public UnityEngine.UI.Image molotovImg;
    public UnityEngine.UI.Image stunImg;
    public UnityEngine.UI.Image bileImg;
    public UnityEngine.UI.Image pipeImg;

    // Start is called before the first frame update
    void Start()
    {
        availableGrenades = new ArrayList();
        availableGrenades.Add(0);
        SelectGrenade();
        //molotovImg.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (grenIndex == availableGrenades.Count - 1)
                grenIndex = 0;
            else
                grenIndex++;
            selectedGrenades = (int)availableGrenades[grenIndex];
            SelectGrenade();
        }
    }

    void SelectGrenade()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedGrenades){
                weapon.gameObject.SetActive(true);
                gren = weapon.gameObject;
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void addGrenade(int x)
    {
        Debug.Log(x);
        if (availableGrenades.Contains(x))
        {
            return;
        }
        else
        {
            availableGrenades.Add(x);
            availableGrenades.Sort();
        }
    }

    public void addAmmo(int x)
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == x)
            {
                weapon.GetComponent<Grenade>().addAmmo();
            }
            i++;
        }
    }
}
