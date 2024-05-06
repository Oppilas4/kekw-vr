using UnityEngine;

public class Tv_collitionSound : MonoBehaviour
{
    public AudioClip collisionSound;
    private AudioSource audioSource;
    bool hasHitOnce = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = collisionSound;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHitOnce)
        {
            audioSource.Play();
        }

        hasHitOnce = true;
    }
}
