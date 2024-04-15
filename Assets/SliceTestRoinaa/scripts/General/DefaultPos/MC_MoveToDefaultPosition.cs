using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_MoveToDefaultPosition : MonoBehaviour
{
    // Store the default spawn location and rotation
    private Vector3 defaultSpawnLocation;
    private Quaternion defaultRotation;
    // Reference to the Rigidbody component
    private Rigidbody rb;

    void Start()
    {
        // Set the default spawn location and rotation to the object's initial position and rotation
        defaultSpawnLocation = transform.position;
        defaultRotation = transform.rotation;
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with the floor's trigger collider
        if (other.gameObject.CompareTag("Floor"))
        {
            // Move the object back to its default spawn location and rotation
            transform.position = defaultSpawnLocation;
            transform.rotation = defaultRotation;
            // Set the Rigidbody's velocity to zero
            rb.velocity = Vector3.zero;
        }
    }
}
