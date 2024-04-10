using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public GameObject objectToSpawn;
    public Vector3 objectRotationWhenSpawned;
}

public class TV_EcoloopSpawner : MonoBehaviour
{
    [SerializeField] ItemData[] items;
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
        ItemData item = items[Random.Range(0, items.Length)];
        GameObject objectToInstantiate = item.objectToSpawn;
        Quaternion rotation = Quaternion.Euler(item.objectRotationWhenSpawned);
        Instantiate(objectToInstantiate, spawnPoint.position, rotation);
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
