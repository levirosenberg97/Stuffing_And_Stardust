using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemShopButtons : MonoBehaviour
{
    public ItemStats item;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI descriptionText;

    private void Start()
    {
        buttonText.text = item.itemName;
        descriptionText.text = item.description;
    }
}
