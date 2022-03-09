using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInfected", menuName = "Infected")]
public class Infected : ScriptableObject
{
    public string type;
    public int attackDamage;
    public int health;
    public int attackSpeed;

}
