using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Tv_EnemySpawner : MonoBehaviour
{
    public List<GameObject> objectsToActivate;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float timeBetweenSpawns = 2.0f;
    public int totalEnemiesToSpawn = 10;
    public float spawnCooldown = 1.0f;

    private int enemiesSpawned = 0;
    private bool isSpawning = false;

    void Start()
    {
        GameObject[] rayInteractors = GameObject.FindObjectsOfType<GameObject>().Where(obj => obj.name == "Ray Interactor").ToArray();

        //fix later

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }
        objectsToActivate.AddRange(rayInteractors);

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnCooldown);

        while (enemiesSpawned < totalEnemiesToSpawn)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
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
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
    }
}
