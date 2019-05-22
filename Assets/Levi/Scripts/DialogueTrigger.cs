using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    public List<Dialogue> dialogue;   

    public void TriggerDialogue(string reason)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, reason);
    }
}
