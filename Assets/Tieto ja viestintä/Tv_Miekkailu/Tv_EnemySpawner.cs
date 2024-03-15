using System.Collections;
using UnityEngine;

public class Tv_EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab, enemy2Prefab;
    public int totalEnemy1ToSpawn, totalEnemy2ToSpawn, allnemies, enemiesSpawned;
    public Transform[] spawnPoints;
    public float timeBetweenSpawns = 2.0f;
    public float spawnCooldown = 1.0f;
    
    private int enemiesMeleeSpawned = 0;
    private int enemiesRangedSpawned = 0;
    private bool isSpawning = false;

    void Start()
    {
        allnemies = totalEnemy1ToSpawn + totalEnemy2ToSpawn;
        enemiesSpawned = totalEnemy1ToSpawn + totalEnemy2ToSpawn;
        StartCoroutine(SpawnEnemies());
        
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnCooldown);

        for (int i = 0; i < allnemies; i++)
        {
            int jees = Random.Range(0, 100);
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if(jees <= 25 && enemiesMeleeSpawned <= totalEnemy1ToSpawn)
            {
                Instantiate(enemy1Prefab, spawnPoint.position, spawnPoint.rotation);
                enemiesMeleeSpawned++;
            }

            else if (enemiesRangedSpawned <= totalEnemy2ToSpawn)
            {
                Instantiate(enemy2Prefab, spawnPoint.position, spawnPoint.rotation);
                enemiesRangedSpawned++;
            }
            
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }


    public void KillEnemy()
    {
        enemiesSpawned--;
        if (!isSpawning && enemiesSpawned <= 0)
        {
            // Activate other objects when all enemies are killed
            gameObject.SetActive(true);
        }
    }
}
