using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public AudioClip LossSound;
    public AudioClip WinSound;
    public AudioSource endCombatSound;
    public List<Button> attackButtons;
    AudioSource bgMusic;

    List<CharacterScript> fighters;
    [SerializeField]
    List<TargetSelector> enemies;
    Animator anim;
    Animator targetAnim;
    LightCrystalTrackingScript crystalTracker;

    public CharacterScript player;

    public DialogueTrigger dialogue;
    public DialogueManager dialogueManager;

    public Animator menuAnim;

    public string mainLevel;

    public int crystals;

    public Image combatScreen;

    public TextMeshProUGUI endScreenText;

    float newHealth;

    private void Start()
    {
        crystalTracker = GameObject.FindObjectOfType<LightCrystalTrackingScript>();
        bgMusic = GetComponent<AudioSource>();
        //enemies = GameObject.FindObjectsOfType<TargetSelector>();
    }

    public void SetEnemies()
    {
        enemies = new List<TargetSelector>(FindObjectsOfType<TargetSelector>());
    }

    public void SetFighters()
    {
        fighters = new List<CharacterScript>(FindObjectsOfType<CharacterScript>());

        ResetCombatOrder();
    }

    public void MakeSelectable()
    {
        switch (player.spell.attackType)
        {
            case AttackStats.AttackType.SingleTarget:
                {

                    foreach (TargetSelector enemy in enemies)
                    {
                        enemy.selectable = true;
                    }

                    break;
                }
            case AttackStats.AttackType.AOE:
                {
                    Combat();

                    break;
                }
            case AttackStats.AttackType.Buff:
                {
                    Combat();
                    break;
                }
        }
    }

    public void MakeUnselectable()
    {
        foreach (TargetSelector enemy in enemies)
        {
            enemy.selectable = false;
        }
        // attackButton.interactable = true;
    }

    public void ResetCombatOrder()
    {
        fighters.Sort(delegate (CharacterScript a, CharacterScript b)
        {
            return (b.stats.speed).CompareTo(a.stats.speed);
        });
    }

    public void Combat()
    {
        menuAnim.SetTrigger("isFading");
        MakeInteractable();
        StartCoroutine("WaitForEndOfAnimation");
    }

    void Attack(float dmg, GameObject target, AttackStats spell)
    {
        if (target != null)
        {
            CharacterScript character = target.GetComponent<CharacterScript>();

            switch (character.element)
            {
                case CharacterScript.ElementalTyping.Water:
                    {
                        if (spell.element == AttackStats.ElementalTyping.Water)
                        {
                            Damage(character, dmg);
                        }
                        else if (spell.element == AttackStats.ElementalTyping.Fire)
                        {
                            HalfDamage(character, dmg);
                        }
                        else if (spell.element == AttackStats.ElementalTyping.Wood)
                        {
                            DoubleDamage(character, dmg);
                        }

                        break;
                    }
                case CharacterScript.ElementalTyping.Fire:
                    {
                        if (spell.element == AttackStats.ElementalTyping.Water)
                        {
                            DoubleDamage(character, dmg);
                        }
                        else if (spell.element == AttackStats.ElementalTyping.Fire)
                        {
                            Damage(character, dmg);
                        }
                        else if (spell.element == AttackStats.ElementalTyping.Wood)
                        {
                            HalfDamage(character, dmg);
                        }
                        break;
                    }
                case CharacterScript.ElementalTyping.Wood:
                    {
                        if (spell.element == AttackStats.ElementalTyping.Water)
                        {
                            HalfDamage(character, dmg);
                        }
                        else if (spell.element == AttackStats.ElementalTyping.Fire)
                        {
                            DoubleDamage(character, dmg);
                        }
                        else if (spell.element == AttackStats.ElementalTyping.Wood)
                        {
                            Damage(character, dmg);
                        }
                        break;
                    }
            }
        }
    }

    void Damage(CharacterScript character, float dmg)
    {
        newHealth = character.healthValue;
        dmg = Random.Range(dmg - 2, dmg + 2);
        dmg = Mathf.RoundToInt(dmg);
        if (dmg < 1)
        {
            dmg = 1;
        }
        newHealth -= dmg;

        StartCoroutine(ReduceHealthBar(newHealth, character));

        if (character.healthValue <= 0)
        {
            FighterDead(character);
        }
    }

    void DoubleDamage(CharacterScript character, float dmg)
    {
        newHealth = character.healthValue;
        dmg = Random.Range(dmg - 2, dmg + 2);
        dmg = Mathf.RoundToInt(dmg);
        if (dmg < 1)
        {
            dmg = 1;
        }
        newHealth -= dmg * 2;

        StartCoroutine(ReduceHealthBar(newHealth, character));


        if (character.healthValue <= 0)
        {
            FighterDead(character);
        }
    }

    void HalfDamage(CharacterScript character, float dmg)
    {
        newHealth = character.healthValue;
        dmg = Random.Range(dmg - 2, dmg + 2);
        dmg =Mathf.RoundToInt(dmg);
        if(dmg < 1)
        {
            dmg = 1;
        }
        newHealth -= dmg - (character.stats.defense / 2);
        newHealth = Mathf.Round(newHealth);
        StartCoroutine(ReduceHealthBar(newHealth, character));
        character.healthSlider.value = character.healthValue;


        if (character.healthValue <= 0)
        {
            FighterDead(character);
        }
    }

    void FighterDead(CharacterScript fighter)
    {
        anim = fighter.GetComponent<Animator>();
        if (anim == null)
        {
            fighter.gameObject.SetActive(false);
        }
        else
        {
            anim.SetTrigger("isDead");
            fighter.particlesPlayed = true;
            fighter.isAlive = false;
        }
        fighter.healthSlider.gameObject.SetActive(false);
        fighter.healthText.gameObject.SetActive(false);
        if (fighter.lightDispenser != null && fighter.particlesPlayed != true)
        {
            fighter.lightDispenser.SetActive(true);
        }
        if(fighter.player == true)
        {
            CombatEnd();
        }
    }

    void MakeInteractable()
    {        
        foreach(Button button in attackButtons)
        {
            if(button.interactable == false)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    void CombatEnd()
    {
        int counter = 0;
        StopCoroutine("WaitForEndOfAnimation");
        menuAnim.SetTrigger("isUnFading");
        MakeInteractable();
        foreach (CharacterScript fighter in fighters)
        {
            if (fighter.enemy == true && fighter.isAlive == false)
            {
                counter++;
            }
            else if (fighter.player == true && fighter.isAlive == false)
            {
                GameObject tracker = GameObject.FindGameObjectWithTag("Tracker");
                if (tracker != null)
                {
                    tracker.GetComponent<LightCrystalTrackingScript>().LoseCrystals(crystals);
                }
                bgMusic.Stop();
                menuAnim.SetBool("IsEnded", true);
                endCombatSound.clip = LossSound;
                endScreenText.text = "You Lose";
                if (crystalTracker != null && crystalTracker.tutorial == true)
                {
                    //ending combat dialogue
                    //dialogue.TriggerDialogue();
                    crystalTracker.tutorial = false;
                    crystalTracker.firstAttack = false;
                }
            }
            if (fighter.player == true)
            {
                anim = fighter.GetComponent<Animator>();
            }
        }
        if (counter == fighters.Count - 1)
        {
            StartCoroutine(WinningAnimation(anim));
        }
        combatScreen.gameObject.SetActive(false);
    }

    IEnumerator WinningAnimation(Animator fighterAnim)
    {
            GameObject tracker = GameObject.FindGameObjectWithTag("Tracker");
            if (tracker != null)
            {
                tracker.GetComponent<LightCrystalTrackingScript>().AddCrystals(crystals);
            }
            fighterAnim.SetTrigger("isWon");
            yield return new WaitForSecondsRealtime(fighterAnim.GetCurrentAnimatorStateInfo(0).length - .5f);
            bgMusic.Stop();
            menuAnim.SetBool("IsEnded", true);
            endCombatSound.clip = WinSound;
            endScreenText.text = "You Win";

            if (crystalTracker != null && crystalTracker.tutorial == true)
            {
                //ending combat dialogue
                dialogue.TriggerDialogue("CombatWon");
                crystalTracker.tutorial = false;
                crystalTracker.firstAttack = false;
            }
    }

    IEnumerator WaitForEndOfAnimation()
    {
        foreach (CharacterScript fighter in fighters)
        {
            if(fighter.isAlive == true)
            {

                anim = fighter.gameObject.GetComponent<Animator>();

                switch (fighter.spell.attackType)
                {
                    case AttackStats.AttackType.SingleTarget:
                        {
                            fighter.transform.LookAt(fighter.target.transform);
                            if (anim != null)
                            {

                                anim.SetTrigger("is" + fighter.spell.attackName);
                                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
                                if (fighter.target.GetComponent<Animator>() != null)
                                {
                                    targetAnim = fighter.target.GetComponent<Animator>();
                                    Attack(fighter.charDmg, fighter.target, fighter.spell);
                                    if (newHealth <= 0)
                                    {
                                        FighterDead(fighter.target.GetComponent<CharacterScript>());
                                        break;
                                    }
                                    targetAnim.SetTrigger("isDamaged");
                                    fighter.target.GetComponent<AudioSource>().Play();
                                    yield return new WaitForSeconds(targetAnim.GetCurrentAnimatorStateInfo(0).length);
                                    if (fighter.enemy == true && crystalTracker != null && crystalTracker.firstAttack == true)
                                    {
                                        //player damage dialogue
                                        dialogue.TriggerDialogue("Concern");

                                        yield return new WaitUntil(() => dialogueManager.pauseCoroutine == false);
                                    }
                                }
                                if (fighter.player == true && crystalTracker != null && crystalTracker.firstAttack == true)
                                {
                                    //after player attack dialogue
                                    dialogue.TriggerDialogue("EnemiesTurn");
                                    yield return new WaitUntil(() => dialogueManager.pauseCoroutine == false);                                    
                                }

                                if (fighter.enemy == true && crystalTracker != null && crystalTracker.firstAttack == true)
                                {
                                    dialogue.TriggerDialogue("Faith");
                                    crystalTracker.firstAttack = false;
                                    yield return new WaitUntil(() => dialogueManager.pauseCoroutine == false);                                    
                                }

                                break;
                            }
                            Attack(fighter.charDmg, fighter.target, fighter.spell);

                            break;
                        }
                    case AttackStats.AttackType.AOE:
                        {
                            //checks if the attacker is ana enemy or not
                            if (fighter.enemy == true)
                            {
                                //makes sure the enemy isnt dead when you attack
                                if (fighter.healthValue > 0)
                                {
                                    if (anim != null)
                                    {
                                        anim.SetTrigger("is" + fighter.spell.attackName);
                                        yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorStateInfo(0).length);
                                        if (fighter.target.GetComponent<Animator>() != null)
                                        {
                                            targetAnim = fighter.target.GetComponent<Animator>();
                                            targetAnim.SetTrigger("isDamaged");
                                            fighter.target.GetComponent<AudioSource>().Play();
                                            Attack(fighter.charDmg, fighter.target, fighter.spell);
                                            yield return new WaitForSecondsRealtime(targetAnim.GetCurrentAnimatorStateInfo(0).length);
                                        }
                                        break;
                                    }

                                    Attack(fighter.charDmg + fighter.stats.spellPower, fighter.target, fighter.spell);
                                }
                            }
                            else if (fighter.player == true)
                            {
                                //plays the attack animation and waits for it to finish before continueing the coroutine
                                anim.SetTrigger("is" + fighter.spell.attackName);
                                
                                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

                                foreach (TargetSelector enemy in enemies)
                                {
                                    targetAnim = enemy.GetComponent<Animator>();
                                    enemy.SetTarget();

                                    Attack(fighter.charDmg, fighter.target, fighter.spell);

                                    if (enemy.GetComponent<Animator>() != null)
                                    {
                                        if (newHealth <= 0)
                                        {
                                            FighterDead(fighter.target.GetComponent<CharacterScript>());
                                        }
                                        else
                                        {
                                            targetAnim.SetTrigger("isDamaged");
                                        }
                                        enemy.GetComponent<AudioSource>().Play();
                                    }
                                }

                                yield return new WaitForSeconds(targetAnim.GetCurrentAnimatorStateInfo(0).length);
                            }
                            if (fighter.player == true && crystalTracker != null && crystalTracker.firstAttack == true)
                            {
                                //after player attack dialogue
                                dialogue.TriggerDialogue("EnemiesTurn");
                                yield return new WaitUntil(() => dialogueManager.pauseCoroutine == false);
                            }

                            break;
                        }
                    case AttackStats.AttackType.Buff:
                        {
                            anim.SetTrigger("is" + fighter.spell.attackName);
                            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

                            if (fighter.spell.element == AttackStats.ElementalTyping.Fire)
                            {
                                fighter.stats.spellPower += fighter.spell.damage;
                            }
                            else if (fighter.spell.element == AttackStats.ElementalTyping.Water)
                            {
                                float newHealth = fighter.healthValue + fighter.spell.damage;

                                StartCoroutine(IncreaseHealthBar(newHealth, fighter));
                            }
                            else if (fighter.spell.element == AttackStats.ElementalTyping.Wood)
                            {
                                fighter.stats.defense += fighter.spell.damage;
                            }

                            if (fighter.player == true && crystalTracker != null && crystalTracker.firstAttack == true)
                            {
                                //after player attack dialogue
                                dialogue.TriggerDialogue("EnemiesTurn`");
                                yield return new WaitUntil(() => dialogueManager.pauseCoroutine == false);
                            }

                            break;
                        }
                }
            }
        }
        CombatEnd();
    }

    IEnumerator ReduceHealthBar(float newHealth, CharacterScript character)
    {
        while (character.healthValue != newHealth)
        {
            character.healthValue--;
            character.healthSlider.value = character.healthValue;
            character.healthText.text = character.healthValue + "/" + character.stats.hp;

            if (character.healthValue <= 0)
            {
                FighterDead(character);
                break;
            }


            if (character.healthValue <= newHealth)
            {
                character.healthValue = newHealth;
                character.healthSlider.value = character.healthValue;
                StopCoroutine(ReduceHealthBar(newHealth, character));
                break;
            }

            yield return new WaitForSecondsRealtime(.01f);
        }
    }

    IEnumerator IncreaseHealthBar(float newHealth, CharacterScript character)
    {
        while (character.healthValue != newHealth)
        {
            character.healthValue++;
            character.healthSlider.value = character.healthValue;
            character.healthText.text = character.healthValue + "/" + character.stats.hp;

            if (character.healthValue <= 0)
            {
                FighterDead(character);
                break;
            }


            if (character.healthValue >= newHealth)
            {
                character.healthValue = newHealth;
                character.healthSlider.value = character.healthValue;
                break;
            }

            yield return new WaitForSecondsRealtime(.01f);
        }
    }

}