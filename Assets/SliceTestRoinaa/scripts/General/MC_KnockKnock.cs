using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_KnockKnock : MonoBehaviour
{
    public AudioClip[] knockSounds;
    public AudioSource audioSource;
    private int knockCount = 0;
    private bool isKnocking = false;
    public int requiredKnockCount = 2;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("LeftHand") || collision.gameObject.CompareTag("RightHand"))
        {
            if (!isKnocking) // To avoid multiple knocks from the same collision
            {
                isKnocking = true;
                StartCoroutine(ResetKnocking());
                knockCount++;
                if (knockCount >= requiredKnockCount)
                {
                    PlayRandomKnockSound();
                    knockCount = 0;
                    // You can put additional actions here, like opening the door
                }
            }
        }
    }

    IEnumerator ResetKnocking()
    {
        yield return new WaitForSeconds(1f); // Adjust the delay as needed
        isKnocking = false;
    }

    void PlayRandomKnockSound()
    {
        if (knockSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, knockSounds.Length);
            audioSource.clip = knockSounds[randomIndex];
            audioSource.Play();
        }
    }
}
