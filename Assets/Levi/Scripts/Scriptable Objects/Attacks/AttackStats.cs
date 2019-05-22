using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="Attack", order = 1)]
public class AttackStats : ScriptableObject
{
    public int damage;
    public string attackName;
    public AudioClip sound;

    public enum ElementalTyping { Water, Fire, Wood };
    public ElementalTyping element;

    public enum AttackType { SingleTarget, AOE, Buff };
    public AttackType attackType;
}
