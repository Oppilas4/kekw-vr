using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tv_OttoBehaviour : MonoBehaviour
{
    public AudioClip[] idleClips;
    public AudioClip triggerEnterClip;

    private AudioSource audioSource;
    private bool isTriggerAudioPlaying = false;
    private List<AudioClip> idleClipsToPlay = new List<AudioClip>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Populate idleClipsToPlay list with all idle clips
        foreach (var clip in idleClips)
        {
            idleClipsToPlay.Add(clip);
        }

        // Start playing idle clips every 20 seconds
        InvokeRepeating("PlayIdleClip", 0f, 20f);
    }

    void OnTriggerEnter(Collider other)
    {
        // Play trigger enter clip and stop playing idle clips
        if (!isTriggerAudioPlaying)
        {
            isTriggerAudioPlaying = true;
            StopAllCoroutines();
            StartCoroutine(PlayTriggerEnterClip());
        }
    }

    IEnumerator PlayTriggerEnterClip()
    {
        audioSource.clip = triggerEnterClip;
        audioSource.Play();
        yield return new WaitForSeconds(triggerEnterClip.length);
        isTriggerAudioPlaying = false;
    }

    void PlayIdleClip()
    {
        if (!isTriggerAudioPlaying && idleClipsToPlay.Count > 0)
        {
            int randomIndex = Random.Range(0, idleClipsToPlay.Count);
            AudioClip clipToPlay = idleClipsToPlay[randomIndex];
            idleClipsToPlay.RemoveAt(randomIndex); // Remove the played clip from the list

            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
        else if (idleClipsToPlay.Count == 0)
        {
            // Reset idleClipsToPlay list if all clips have been played
            idleClipsToPlay.Clear();
            foreach (var clip in idleClips)
            {
                idleClipsToPlay.Add(clip);
            }
        }
    }
}
