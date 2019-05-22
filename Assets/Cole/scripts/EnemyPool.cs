using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    public List<GameObject> PooledEnemies1;
    public List<GameObject> PooledEnemies2;
    public List<GameObject> PooledEnemies3;
    public List<GameObject> PooledEnemySpots;

    public GameObject EnemyToPool1;
    public GameObject EnemyToPool2;
    public GameObject EnemyToPool3;
    public GameObject EnemySpotsPool;

    public int AmountToPool1;
    public int AmountToPool2;
    public int AmountToPool3;
    public int SpotCount;

    // Use this for initialization
    void Start ()
    {
        //pool 1
        PooledEnemies1 = new List<GameObject>();
        for(int i = 0; i < AmountToPool1; i++)
        {
            GameObject obj = (GameObject)Instantiate(EnemyToPool1);
            obj.SetActive(false);
            PooledEnemies1.Add(obj);
        }

        //pool 2
        PooledEnemies2 = new List<GameObject>();
        for (int i = 0; i < AmountToPool2; i++)
        {
            GameObject obj = (GameObject)Instantiate(EnemyToPool2);
            obj.SetActive(false);
            PooledEnemies2.Add(obj);
        }

        //Pool 3
        PooledEnemies3 = new List<GameObject>();
        for (int i = 0; i < AmountToPool3; i++)
        {
            GameObject obj = (GameObject)Instantiate(EnemyToPool3);
            obj.SetActive(false);
            PooledEnemies1.Add(obj);
        }

        //Encounter spots pool
        PooledEnemySpots = new List<GameObject>();
        for (int i = 0; i < SpotCount; i++)
        {
            GameObject obj = (GameObject)Instantiate(EnemySpotsPool);
            obj.SetActive(false);
            PooledEnemySpots.Add(obj);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public GameObject GetPooledObject1()
    {
        for(int i = 0; i < PooledEnemies1.Count; i++)
        {
            if(!PooledEnemies1[i].activeInHierarchy)
            {
                return PooledEnemies1[i];
            }
        }
        return null;
    }

    public GameObject GetPooledObject2()
    {
        for (int i = 0; i < PooledEnemies2.Count; i++)
        {
            if (!PooledEnemies2[i].activeInHierarchy)
            {
                return PooledEnemies2[i];
            }
        }
        return null;
    }

    public GameObject GetPooledObject3()
    {
        for (int i = 0; i < PooledEnemies3.Count; i++)
        {
            if (!PooledEnemies3[i].activeInHierarchy)
            {
                return PooledEnemies3[i];
            }
        }
        return null;
    }

    public GameObject getPooledEnemySpots()
    {
        for (int i = 0; i < PooledEnemySpots.Count; i++)
        {
            if (!PooledEnemySpots[i].activeInHierarchy)
            {
                return PooledEnemySpots[i];
            }
        }
        return null;
    }

    /*
     * https://www.raywenderlich.com/847-object-pooling-in-unity
     * T used for returning too pool
     * if (other.gameObject.tag == "Boundary") 
     * {
  if (gameObject.tag == "Player Bullet") 
  {
    gameObject.SetActive(false);
  } 
  else 
  {
    Destroy(gameObject);
  }
}
*/
}
