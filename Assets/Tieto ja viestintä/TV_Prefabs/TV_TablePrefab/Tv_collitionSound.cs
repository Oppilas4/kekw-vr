using UnityEngine;

public class Tv_collitionSound : MonoBehaviour
{
    public AudioClip collisionSound; // Sound to play on collision
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If AudioSource is not attached, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the collision sound
        audioSource.clip = collisionSound;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Play the collision sound when collision occurs
        audioSource.Play();
    }

    // If you're using triggers instead of collisions, use OnTriggerEnter
    // private void OnTriggerEnter(Collider other)
    // {
    //     // Play the collision sound when trigger occurs
    //     audioSource.Play();
    // }
}
