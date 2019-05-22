using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Item", order = 1)]
public class ItemStats : ScriptableObject
{
    public string itemName;
    public string description;

    public int spellPower;
    public int defense;

    public int healthIncrease;
}
