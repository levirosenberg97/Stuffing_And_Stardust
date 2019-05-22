using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellScript : MonoBehaviour
{
    public AttackStats attack;
    public int dmg;
    public CharacterScript player;
    Button button;
    AudioSource audioSource;
    float charDmg;

    void Start()
    {
        button = GetComponent<Button>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = attack.attackName;

        audioSource = GetComponent<AudioSource>();
        if (attack.sound != null)
        {
            audioSource.clip = attack.sound;
        }

        charDmg = player.charDmg;
    }

    public void SelectSpell()
    {
        player.spell = attack;
        player.charDmg = charDmg + attack.damage;
    }

}