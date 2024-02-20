using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_SpawnTest : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject spawnPrefab;

    // Update is called once per frame
    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        // Check if the spawnPrefab is assigned
        if (spawnPrefab != null)
        {
            // Instantiate the prefab at the spawnPoint position
            GameObject newPrefabInstance = Instantiate(spawnPrefab, spawnPoint.position, Quaternion.identity);

            // Optionally, you can do further customization or setup for the spawned prefab here
        }
        else
        {
            Debug.LogWarning("Prefab not assigned in the inspector!");
        }
    }
}
