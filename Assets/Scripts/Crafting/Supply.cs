using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Crafting-Inventory/Supply")]
public class Supply : ScriptableObject
{
    new public string name;
    public int count; 
}
