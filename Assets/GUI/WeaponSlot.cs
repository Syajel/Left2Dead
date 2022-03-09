using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Slot", menuName = "New WeaponSlot")]
public class WeaponSlot : ScriptableObject
{
    public new string name;
    public Sprite sprite;
}
