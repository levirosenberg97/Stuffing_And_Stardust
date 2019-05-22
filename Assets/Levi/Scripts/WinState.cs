using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    DialogueTrigger trigger;
    LightCrystalTrackingScript crystalTracker;

    public GameObject creditsScreen;
    public GameObject menuButton;
    public Animator pauseAnim;

    public float maxCrystals;

    bool won;

    private void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
        crystalTracker = GameObject.FindObjectOfType<LightCrystalTrackingScript>();
        if (crystalTracker != null)
        {
            if (crystalTracker.currentCrystals >= maxCrystals)
            {
                trigger.TriggerDialogue("Won");
                won = true;
            }
        }
    }

    private void Update()
    {
        if (trigger != null)
        {
            if (trigger.dialogue.Count == 0 && won == true)
            {
                pauseAnim.SetTrigger("isFading");
                Cursor.visible = true;
                menuButton.SetActive(true);
            }
        }
    }
}
