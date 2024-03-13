using System.Collections;
using UnityEngine;

public class Tv_EnemySpawner : MonoBehaviour
{
    public GameObject teleporter;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float timeBetweenSpawns = 2.0f;
    public int totalEnemiesToSpawn = 10;
    public float spawnCooldown = 1.0f;

    private int enemiesSpawned = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnCooldown);

        while (enemiesSpawned < totalEnemiesToSpawn)
        {
            // Check if spawning is allowed
            if (!isSpawning)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
                continue;
            }

            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate enemy at the spawn point
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            enemiesSpawned++;

            // Wait for the next spawn
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
            teleporter.SetActive(true);
        }
    }
}
