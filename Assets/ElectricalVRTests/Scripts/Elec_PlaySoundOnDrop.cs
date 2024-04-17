using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_PlaySoundOnDrop : MonoBehaviour
{
    Rigidbody body;
    public AudioClip clip;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (body.velocity.magnitude > 0 && collision.transform.GetComponent<Rigidbody>() == null) audioSource.PlayOneShot(clip);
    }
}
