using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_SakariTalk : MonoBehaviour
{
    AudioSource audioSource;
    bool isPlayingSpecialClip = false;

    [SerializeField] AudioClip heyYou;
    [SerializeField] AudioClip bottle;
    [SerializeField] AudioClip[] idleClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayIdleClips());
    }

    IEnumerator PlayIdleClips()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f); // Wait for 15 seconds
            if (!isPlayingSpecialClip)
            {
                // Play a random idle clip
                int randomIndex = Random.Range(0, idleClips.Length);
                audioSource.clip = idleClips[randomIndex];
                audioSource.Play();
            }
        }
    }

    public void ActivateHeyYou()
    {
        if (!isPlayingSpecialClip)
        {
            isPlayingSpecialClip = true;
            audioSource.clip = heyYou;
            audioSource.Play();
            StartCoroutine(ResetSpecialClipFlag(heyYou.length));
        }
    }

    public void ActivateBottle()
    {
        if (!isPlayingSpecialClip)
        {
            isPlayingSpecialClip = true;
            audioSource.clip = bottle;
            audioSource.Play();
            StartCoroutine(ResetSpecialClipFlag(bottle.length));
        }
    }

    IEnumerator ResetSpecialClipFlag(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        isPlayingSpecialClip = false;
    }
}
