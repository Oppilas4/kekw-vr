using System.Collections;
using UnityEngine;

public class Tv_EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab, enemy2Prefab;
    public int totalEnemy1ToSpawn, totalEnemy2ToSpawn;
    public Transform[] spawnPoints;
    public float timeBetweenSpawns = 2.0f;
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

        // Spawn enemy1Prefab
        for (int i = 0; i < totalEnemy1ToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy1Prefab, spawnPoint.position, spawnPoint.rotation);
            enemiesSpawned++;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        // Spawn enemy2Prefab
        for (int i = 0; i < totalEnemy2ToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy2Prefab, spawnPoint.position, spawnPoint.rotation);
            enemiesSpawned++;
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
