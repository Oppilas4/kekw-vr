using System.Collections;
using UnityEngine;

public class Tv_EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab, enemy2Prefab, teleport;
    public int totalEnemy1ToSpawn, totalEnemy2ToSpawn, allEnemies, enemiesSpawned;
    public Transform[] spawnPoints;
    
    public float timeBetweenSpawns = 2.0f;
    public float spawnCooldown = 1.0f;
    
    public int enemiesMeleeSpawned = 0;
    public int enemiesRangedSpawned = 0;
    private bool isSpawning = false;
    public AudioSource winAudio;
    public AudioSource spawnSound;
    public AudioSource killSound;
    public AudioSource Music;

    [SerializeField] bool enemiesPartrol = false;

    void Start()
    {
        allEnemies = totalEnemy1ToSpawn + totalEnemy2ToSpawn;
        enemiesSpawned = totalEnemy1ToSpawn + totalEnemy2ToSpawn;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnCooldown);

        for (int i = 0; i < allEnemies; i++)
        {
            // Randomly select a spawn point and enemy type
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyPrefab;
            if (Random.Range(0f, 1f) < 0.5f)
            {
                enemyPrefab = enemy1Prefab;
            }
            else
            {
                enemyPrefab = enemy2Prefab;
            }

            // Instantiate the selected enemy prefab at the chosen spawn point
            GameObject enemyHEHE = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemyHEHE.GetComponent<Tv_EnemyMovement>().runAway = enemiesPartrol;
            spawnSound.Play();

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
        killSound.Play();

        if (!isSpawning && enemiesSpawned <= 0)
        {
            Invoke("PlayAudio", 1f);
            
            teleport.SetActive(true);    
        }
    }

    void PlayAudio()
    {
        winAudio.Play();
        Music.volume = 0.04f;
    }
}
