using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LightCrystalTrackingScript : MonoBehaviour
{
    public int lightCrystals;
    public int currentCrystals;
    int startingCrystals;

    public Vector3 playerPos;

    public int currentEnemies;
    public bool tutorial;
    public bool firstAttack;
    GameObject lightCounter;

    void Start ()
    {
        tutorial = true;
        firstAttack = true;
        currentCrystals = lightCrystals;
        startingCrystals = lightCrystals;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Tracker");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
	}

    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "OverWorld")
        {
            if (GameObject.FindGameObjectWithTag("Dialogue") != null)
            {
                DialogueTrigger dialogueTrigger;
                dialogueTrigger = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueTrigger>();
                if (tutorial == true)
                {
                    dialogueTrigger.TriggerDialogue("Intro");
                }
                else
                {
                    GameObject.FindObjectOfType<PlayerController>().isPlayable = true;
                    dialogueTrigger.dialogue.Remove(dialogueTrigger.dialogue[0]);
                }
            }
            FindObjectOfType<ShadowControlScript>().lightAdded = true;
            lightCounter = GameObject.FindGameObjectWithTag("StarCounter");
            if(lightCounter != null)
            {
                lightCounter.GetComponent<TextMeshProUGUI>().text = "X " + currentCrystals.ToString();
            }
            FindObjectOfType<PlayerController>().gameObject.transform.position = playerPos;
        }  
        if(SceneManager.GetActiveScene().name == "CombatScene")
        {
            if(tutorial == true)
            {
                if (GameObject.FindGameObjectWithTag("Dialogue") != null)
                {
                    DialogueTrigger dialogueTrigger;
                    dialogueTrigger = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueTrigger>();
                    dialogueTrigger.TriggerDialogue("Tutorial");
                }

            }

            lightCounter = GameObject.FindGameObjectWithTag("StarCounter");
            if (lightCounter != null)
            {
                lightCounter.GetComponent<TextMeshProUGUI>().text = "X " + currentCrystals.ToString();
            }
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            tutorial = true;
            playerPos = new Vector3(5, 0, 3);
            currentCrystals = startingCrystals;
            lightCrystals = startingCrystals;
            Cursor.visible = true;
        }
    }

    public void AddCrystals(int crystals)
    {
        lightCrystals = currentCrystals;
        currentCrystals = crystals + lightCrystals;
        if (lightCounter != null)
        {
            lightCounter.GetComponent<TextMeshProUGUI>().text = "X " + currentCrystals.ToString();
        }
    }

    public void LoseCrystals(int crystals)
    {
        lightCrystals = currentCrystals;
        currentCrystals -= Mathf.RoundToInt(crystals / 2);

        if(currentCrystals <= 0)
        {
            currentCrystals = 0;
            SceneManager.LoadScene("LoseScene");
        }

        if (lightCounter != null)
        {
            lightCounter.GetComponent<TextMeshProUGUI>().text = "X " + currentCrystals.ToString();
        }
    }
}
