using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battletrigger : MonoBehaviour
{
    public AudioSource AudioFile;

    GameObject DDOL;

    LightCrystalTrackingScript CrystalTracker;

    private void Start()
    {
        DDOL = GameObject.FindGameObjectWithTag("Tracker");

        if (CrystalTracker != null)
        {
            DDOL.GetComponent<LightCrystalTrackingScript>().playerPos = transform.position;
        }

    }


    //DDOL = Don't Destroy On Load

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {

            if (CrystalTracker != null)
            {
                CrystalTracker.playerPos = transform.position;

                if (CrystalTracker.currentCrystals >= 12)
                {
                    SceneManager.LoadScene("BossScene");
                }
            }

                else
                {
                    AudioFile.Play();
                    Cursor.visible = true;
                    SceneManager.LoadScene("CombatScene");
                    Debug.Log("Fight Time");
                    Destroy(gameObject);
                }
        }

            Debug.Log("That's not it chief");
      
        //T fix debug pos

    }
}
