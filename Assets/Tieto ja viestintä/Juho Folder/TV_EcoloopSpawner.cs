using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_EcoloopSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform spawnPoint;
    public float minSpawnDelay = .7f;
    public float maxSpawnDelay = 1.5f;

    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObjects();
            SetNextSpawnTime();
        }
    }

    void SpawnObjects()
    {
        GameObject objectToInstantiate = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Instantiate(objectToInstantiate, spawnPoint.position, spawnPoint.rotation);
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
