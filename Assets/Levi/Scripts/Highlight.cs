using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Highlight : MonoBehaviour
{
    Text buttonText;
    public Color highlightColor;
    public Color selectedColor;


	void Start ()
    {
        buttonText = GetComponentInChildren<Text>();      
	}

    public void ChangeColor()
    {
        if (buttonText.color != selectedColor)
        {
            buttonText.color = highlightColor;
        }
    }

    public void ReturnColor()
    {
        if (buttonText.color != selectedColor)
        {
            buttonText.color = Color.white;
        }
    }    
    

    public void SelectedColor()
    {
        buttonText.color = selectedColor;
    }

}
