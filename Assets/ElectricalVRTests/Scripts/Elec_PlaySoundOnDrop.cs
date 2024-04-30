using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_PlaySoundOnDrop : MonoBehaviour
{
    Rigidbody body;
    public AudioClip clip;
    AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (body.velocity.magnitude > 0 && collision.gameObject.transform.GetComponent<Rigidbody>() == null && clip != null) audioSource?.PlayOneShot(clip);
    }
}
