using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseCanvas;
    public float speed;

    public bool moveMenu = false;
    public Animator anim;
    Vector2 startPos;
    bool creditsActive = false;

    private void Start()
    {
        //start position of the image
        startPos = pauseCanvas.gameObject.transform.position;
    }

    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(moveMenu != true)
            {
                moveMenu = true;               
            }
            else
            {
                moveMenu = false;
                if(creditsActive == true)
                {
                    FadeCredits();
                }
            }
        }

        if(moveMenu == true)
        {
            SetPosition();
        }
        else if(moveMenu == false)
        {
            ResetPosition();
        }
	}


    void SetPosition()
    {
        pauseCanvas.gameObject.transform.localPosition = Vector2.Lerp(pauseCanvas.gameObject.transform.localPosition, Vector2.zero, Time.deltaTime * speed);
    }

    void ResetPosition()
    {
        pauseCanvas.transform.position = Vector2.Lerp(pauseCanvas.gameObject.transform.position, startPos, Time.deltaTime * speed);
    }

    public void ResumeButton()
    {
        moveMenu = false;

        Cursor.visible = false;
    }

    public void FadeCredits()
    {
        if(creditsActive == false)
        {
            anim.SetTrigger("isFading");
            creditsActive = true;
        }
        else
        {
            anim.SetTrigger("isUnFading");
        }
    }
}
