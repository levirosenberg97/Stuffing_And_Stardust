using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<Vector3> startPositions;
    public List<Quaternion> startRotations;

    Button enemyButton;
    public GameObject combatScreen;
    public AudioSource audioSource;
    CombatManager combatManager;


    private void Start()
    {
        combatManager = GetComponent<CombatManager>();

        int amountSpawned = Random.Range(1, 3);

        if (amountSpawned <= 1)
        {
            
            int rand = Random.Range(0, enemies.Count);
            GameObject enemy = Instantiate(enemies[rand], startPositions[2],startRotations[2]);
            enemyButton = enemy.GetComponentInChildren<Button>();
            enemyButton.onClick.AddListener(EnemyInteractable);
            
        }
        else
        {
            int rand1 = Random.Range(0, enemies.Count);
            int rand2 = Random.Range(0, enemies.Count);

            GameObject enemy1 = Instantiate(enemies[rand1], startPositions[0], startRotations[0]);
            enemyButton = enemy1.GetComponentInChildren<Button>();
            enemyButton.onClick.AddListener(EnemyInteractable);


            GameObject enemy2 = Instantiate(enemies[rand2], startPositions[1], startRotations[1]);
            enemyButton = enemy2.GetComponentInChildren<Button>();
            enemyButton.onClick.AddListener(EnemyInteractable);
        }
        combatManager.SetEnemies();
        combatManager.SetFighters();
    }

    void EnemyInteractable()
    {
        combatManager.MakeUnselectable();
        combatManager.Combat();
        combatScreen.SetActive(false);
        audioSource.Play();
    }

}
