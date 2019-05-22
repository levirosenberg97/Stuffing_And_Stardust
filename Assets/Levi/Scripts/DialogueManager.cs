using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator anim;
    public bool pauseCoroutine = false;

    PlayerController player;

    private Queue<string> sentences;

    List<Dialogue> dialogueList;
   // int currentCharStore;
    bool speaking;
    
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "OverWorld")
        {
            player = GameObject.FindObjectOfType<PlayerController>();
        }
    }

    private void Update()
    {
        if(speaking == true)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(List<Dialogue> dialogue, string reason)
    {
        for (int i = 0; i < dialogue.Count; i++)
        {

            if (dialogue[i].reason == reason)
            {
                if (player != null)
                {
                    player.isPlayable = false;
                }
                speaking = true;
                pauseCoroutine = true;
                sentences = new Queue<string>();
                //Debug.Log("Starting talks with " + dialogue[i].name);
                dialogueList = dialogue;
                //currentCharStore = currentChar;
                nameText.text = dialogue[i].name;

                anim.SetBool("IsOpen", true);

                sentences.Clear();

                foreach (string sentence in dialogue[i].sentences)
                {
                    sentences.Enqueue(sentence);
                }

                DisplayNextSentence();
            }
        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines(); 
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        if(player != null)
        {
            player.isPlayable = true;
            Cursor.visible = false;
        }
        Debug.Log("End of Conversation");
        speaking = false;
        dialogueList.Remove(dialogueList[0]);
        pauseCoroutine = false;
        anim.SetBool("IsOpen", false);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
