using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tv_JoonasAudioManager : MonoBehaviour
{
    private Queue<AudioClip> audioQueue = new Queue<AudioClip>();
    private bool isPlaying = false;

    void Update()
    {
        // Check if audio is playing, if not, play the next queued audio
        if (!isPlaying && audioQueue.Count > 0)
        {
            AudioClip clip = audioQueue.Dequeue();
            StartCoroutine(PlayAudio(clip));
        }
    }

    public void PlayVoiceline(AudioClip clip)
    {
        audioQueue.Enqueue(clip);
    }

    IEnumerator PlayAudio(AudioClip clip)
    {
        isPlaying = true;
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
        isPlaying = false;
    }
}