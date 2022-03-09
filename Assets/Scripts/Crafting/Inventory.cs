using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    void Awake() {
        if(instance != null){
            Debug.LogWarning("Found more than one Inventory!");
            return;
        }
        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();
    public List<Supply> supplies = new List<Supply>();

    void Start(){
        //reset items and supplies
        foreach (var item in items)
        {
            item.itemCount = 0;
        }
        foreach (var supply in supplies)
        {
            supply.count = 0;
        }
    }

    //When item is picked
    public void AddItem(string itemName, int amount){
        foreach (Item i in items)
            if(i.name.Equals(itemName))
                i.itemCount += amount;
    }

    //When item is used
    public void UseItem(string itemName, int amount){
        foreach (Item i in items)
            if(i.name.Equals(itemName))
                i.itemCount -= amount;
    }

    //When supply is picked
    public void AddSupply(string supplyName, int amount){
        foreach (Supply i in supplies)
            if(i.name.Equals(supplyName))
                i.count += amount;
    }

    //When supply is used
    public void UseSupply(string supplyName, int amount){
        foreach (Supply i in supplies)
            if(i.name.Equals(supplyName))
                i.count -= amount;
    }
}
