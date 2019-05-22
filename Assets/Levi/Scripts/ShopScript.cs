using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    LightCrystalTrackingScript lightTracker;

    //public List<ItemStats> items;
    public GameObject shopCanvas;
    public ObjectMouseOver mouseOverObject;
    public CharacterScript player;

    private void Start()
    {
        lightTracker = GameObject.FindObjectOfType<LightCrystalTrackingScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hit");
            mouseOverObject.selectable = true;
        }
    }

    public void ReceiveItem(ItemStats itemStats)
    {
        player.stats.defense += itemStats.defense;
        player.stats.spellPower += itemStats.spellPower;
        player.healthValue += itemStats.healthIncrease;
    }

    public void Buy(int price)
    {
        if(lightTracker != null)
        {
            if (lightTracker.currentCrystals >= price)
            {
                lightTracker.LoseCrystals(price);
            }
            //add an else statement

        }
    }

    public void EnterShop()
    {
        shopCanvas.SetActive(true);
    }

    public void ExitShop()
    {
        shopCanvas.SetActive(false);
        mouseOverObject.selectable = true;
    }

}
