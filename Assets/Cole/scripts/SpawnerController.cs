using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

    
    public int DesiredSpots;

    public GameObject EnemySpots;

    public GameObject BossSpot;

    public float SpawnRadius;

    int KillGoal = 1;

    int ActiveEnemies;

    GameObject DDOL;

    LightCrystalTrackingScript CrystalTracker;

	// Use this for initialization
	void Start ()
    {
        DDOL = GameObject.FindGameObjectWithTag("Tracker");
        if (DDOL != null)
        {
            CrystalTracker = DDOL.GetComponent<LightCrystalTrackingScript>();
        }
   
        SpawnReset();
        for (int i = 0; i < DesiredSpots; i++)
        {
            //float angle = i * Mathf.PI * 2 / DesiredSpots;
           // Vector3 pos = new Vector3
             
        }
        
    }
	
	// Update is called once per frame
	void SpawnReset ()
    {

        if (CrystalTracker != null)
        {
            CrystalTracker.currentEnemies = ActiveEnemies;


            if (CrystalTracker.currentCrystals >= 12)
            {
                Instantiate(BossSpot, transform.position, transform.rotation);
                
            }
            
            else if (ActiveEnemies < KillGoal)
            {
                Instantiate(EnemySpots, transform.position, transform.rotation);
                ActiveEnemies++;
               
            }

            else
            {
                KillGoal++;
            }
        }

        //T make sure DDOL has amount of active enemy spots to save
        //currentCrystals	
    }
}
