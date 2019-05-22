using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMouseOver : MonoBehaviour
{
    public Image image;
    public bool selectable;
    public bool isStore;

    bool startFade;

    private void Start()
    {
        selectable = false;
    }

    private void OnMouseExit()
    {
        startFade = false;
        StartCoroutine(FadeOut());
    }

    private void OnMouseDown()
    {
        if (selectable == true)
        {
            if (isStore == true)
            {
                GetComponentInChildren<ShopScript>().EnterShop();
                selectable = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if (selectable == false)
        {
            startFade = false;
            StartCoroutine(FadeOut());
        }
    }

    private void OnMouseEnter()
    {
        if (selectable == true)
        {            
            startFade = true;
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeOut()
    {
        while (image.color != Color.clear && startFade == false)
        {
            image.color = Color.Lerp(image.color, Color.clear, Time.deltaTime * 10);

            if (image.color == Color.clear || startFade == true)
            {
                break;
            }
            yield return new WaitForSeconds(.0001f);
        }
    }

    IEnumerator FadeIn()
    {
        while (image.color != Color.white && startFade == true)
        {
            image.color = Color.Lerp(image.color, Color.white, Time.deltaTime * 10);

            if (image.color == Color.white || startFade == false)
            {
                break;
            }
            yield return new WaitForSeconds(.0001f);
        }
    }
}
