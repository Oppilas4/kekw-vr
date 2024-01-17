using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_PeelingPotato : MonoBehaviour
{
    public GameObject decalPrefab; // Assign the decal prefab in Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the "Potato" tag
        if (other.CompareTag("Potato"))
        {
            // Get the position where the raycast hits
            Vector3 hitPoint = other.ClosestPointOnBounds(transform.position);

            // Shoot a raycast from the object towards the hit point
            RaycastHit hit;
            if (Physics.Raycast(transform.position, hitPoint - transform.position, out hit))
            {
                // Check if the raycast hits an object with the tag "Potato"
                if (hit.collider.CompareTag("Potato"))
                {
                    // Spawn the Test prefab at the hit point
                    GameObject spawnedTest = Instantiate(decalPrefab, hit.point, Quaternion.identity);

                    // Set the spawned Test prefab as a child of the potato
                    spawnedTest.transform.parent = hit.collider.transform;

                    // Calculate the direction vector from the decal to the potato
                    Vector3 direction = hit.collider.transform.position - spawnedTest.transform.position;

                    // Create an upwards direction vector
                    Vector3 upwards = Vector3.up;

                    // Calculate the desired rotation
                    Quaternion desiredRotation = Quaternion.LookRotation(direction, upwards);

                    // Set the rotation of the decal projector
                    spawnedTest.transform.rotation = desiredRotation;
                }
            }
        }
    }
}
