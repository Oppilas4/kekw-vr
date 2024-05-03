using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_EnemyTutorial : MonoBehaviour
{
    public GameObject enemyToSpawn;
    GameObject currentEnemy;
    bool hasStartedToSpawn = false;
    [SerializeField] Transform whereToSpawn;
    [SerializeField] AudioSource audioSour;

    private void Update()
    {
        if (currentEnemy == null && !hasStartedToSpawn)
        {
            StartCoroutine(SpawnEnemyAfterDelay(1.5f));
        }
    }

    IEnumerator SpawnEnemyAfterDelay(float delay)
    {
        hasStartedToSpawn = true;
        yield return new WaitForSeconds(delay);
        currentEnemy = Instantiate(enemyToSpawn, whereToSpawn.position, whereToSpawn.rotation);
        audioSour.Play();
        hasStartedToSpawn = false;
    }
}
