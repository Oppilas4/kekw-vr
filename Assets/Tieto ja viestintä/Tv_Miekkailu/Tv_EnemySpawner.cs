using System.Collections;
using UnityEngine;

public class Tv_EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public int numberOfEnemiesToSpawn = 5;
    public float spawnDelay = 1.0f;
    [SerializeField] float waitTimeFromStart = 5f;


    public int currentEnemies;

    [SerializeField] GameObject[] objectsToActivateAfterWin;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        currentEnemies = numberOfEnemiesToSpawn;
    }

    public void CheckIfWin()
    {
        if(currentEnemies == 0)
        {
            foreach (GameObject obj in objectsToActivateAfterWin)
            {
                obj.SetActive(true);
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(waitTimeFromStart);

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void KillEnemy()
    {
        currentEnemies = currentEnemies - 1;
        CheckIfWin();   
    }
}
