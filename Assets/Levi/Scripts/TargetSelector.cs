using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelector : MonoBehaviour
{
    CharacterScript player;
    public GameObject spotLight;
    public Button enemyInteractable;
    public bool selectable;
    Camera cam;
    CharacterScript character;

    private void Start()
    {
        character = GetComponent<CharacterScript>();
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterScript>();

        //enemyInteractable.transform.position = cam.WorldToScreenPoint(transform.position);
        enemyInteractable.gameObject.SetActive(false);
    }


    private void Update()
    {      
        if (selectable == true)
        { 
            if(character.isAlive == true)
            {
                enemyInteractable.gameObject.SetActive(true);
            }
            else
            {
                enemyInteractable.gameObject.SetActive(false);
            }
        }
        else
        {
            enemyInteractable.gameObject.SetActive(false);
            spotLight.SetActive(false);
        }     
    }

    private void OnDisable()
    {
        if (spotLight != null)
        {
            spotLight.SetActive(false);
        }
    }

    public void SetTarget()
    {
        player.target = gameObject;
        selectable = false;
    }

}
