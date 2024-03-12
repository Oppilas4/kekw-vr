using System.Collections;
using UnityEngine;

[System.Serializable]
public class Round
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
}

public class Tv_EnemySpawner : MonoBehaviour
{
    public float spawnDelay = 1.0f;
    [SerializeField] float waitTimeFromStart = 5f;

    public Round[] rounds;

    private int currentRoundIndex = 0;
    private int currentEnemies;
    private bool isSpawning = false;

    [SerializeField] GameObject[] objectsToActivateAfterWin;

    void Start()
    {
        StartCoroutine(SpawnNextRound());
    }

    IEnumerator SpawnNextRound()
    {
        yield return new WaitForSeconds(waitTimeFromStart);

        while (currentRoundIndex < rounds.Length)
        {
            Round currentRound = rounds[currentRoundIndex];

            // Spawn enemies for the current round
            foreach (Transform spawnPoint in currentRound.spawnPoints)
            {
                GameObject enemyPrefab = currentRound.enemyPrefabs[Random.Range(0, currentRound.enemyPrefabs.Length)];
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                currentEnemies++;
            }

            isSpawning = true;

            // Wait until all enemies from the current round are killed
            while (currentEnemies > 0)
            {
                yield return null;
            }

            currentRoundIndex++;

            isSpawning = false;

            yield return null; // Optional delay before starting the next round
        }

        // If there are no more rounds, activate objects for win condition
        foreach (GameObject obj in objectsToActivateAfterWin)
        {
            obj.SetActive(true);
        }
    }

    public void KillEnemy()
    {
        currentEnemies--;
        if (!isSpawning && currentEnemies <= 0)
        {
            StartCoroutine(SpawnNextRound());
        }
    }
}
