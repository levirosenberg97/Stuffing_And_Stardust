using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //public List<Vector2> positions;
    //public List<Button> buttons;
    //public List<TargetSelector> enemies;
    //public List<Transform> interactables;

    public Camera mainCam;
    int prevPos;
    float startTime;
    public Image attackMenu;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //void setEnemyInteractable()
    //{
    //    for (int i = 0; i < enemies.Count; i++)
    //    {
    //        Vector2 screenPosition = mainCam.WorldToScreenPoint(enemies[i].transform.position);
    //        interactables[i].position = screenPosition;
    //    }
    //}

    public void ChangeScene(string str)
    {
        SceneManager.LoadScene(str);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void FadeIn()
    {
        anim.SetBool("isFading", true);
    }

    public void FadeOut()
    {
        anim.SetBool("isFading", false);
    }
}
