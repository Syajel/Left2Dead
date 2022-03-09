using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Crafting-Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public string description;
    public Sprite icon;
    public int damage;
    public int itemCount;

    public int maxAmmount;
}
