using System.Collections;
using UnityEngine;

public class Tv_EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab, enemy2Prefab, teleport;
    public int totalEnemy1ToSpawn, totalEnemy2ToSpawn, allnemies, enemiesSpawned;
    public Transform[] spawnPoints;
    public float timeBetweenSpawns = 2.0f;
    public float spawnCooldown = 1.0f;
    
    public int enemiesMeleeSpawned = 0;
    public int enemiesRangedSpawned = 0;
    private bool isSpawning = false;
    public AudioSource winAudio;
    public AudioSource spawnSound;
    public AudioSource killSound;

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
            
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if (totalEnemy1ToSpawn > enemiesMeleeSpawned)
            {        
                Instantiate(enemy1Prefab, spawnPoint.position, spawnPoint.rotation);
                spawnSound.Play();
                enemiesMeleeSpawned++;
            }

             if (totalEnemy2ToSpawn  > enemiesRangedSpawned)
            {
                Instantiate(enemy2Prefab, spawnPoint.position, spawnPoint.rotation);
                spawnSound.Play();
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
    }
}
