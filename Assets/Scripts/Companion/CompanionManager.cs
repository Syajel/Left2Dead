using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CompanionManager : MonoBehaviour
{

    public List<GameObject> companions = new List<GameObject>();
    [HideInInspector]public CompanionController instance;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in companions)
        {
            if(CompanionSelectionMenu.SelectedCompanion.Equals(item.name, StringComparison.InvariantCultureIgnoreCase))
            {
                GameObject i = Instantiate(item, Camera.main.transform.position + new Vector3(1,0,1), 
                Quaternion.identity);
                i.SetActive(true);
                instance = i.GetComponent<CompanionController>();
                break;
            }
        }
    }
}
