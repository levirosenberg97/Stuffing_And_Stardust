using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ObjectStats", order = 2)]
public class ObjectStats : ScriptableObject
{
    public float hp;
    public float spellPower;
    public int speed;
    public int defense;
}
