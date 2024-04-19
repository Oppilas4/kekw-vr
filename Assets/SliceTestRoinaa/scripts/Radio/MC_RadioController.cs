using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_RadioController : MonoBehaviour
{
    public List<AudioClip> audioClips;

    private AudioSource _audioSource;
    private int _currentClipIndex = 0;
    private bool _isPaused = false; // Add a flag to track if the music is paused

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Shuffle(audioClips);
        _audioSource.clip = audioClips[_currentClipIndex];
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
        
    }

    void Shuffle(List<AudioClip> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            AudioClip value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    IEnumerator CheckIfTrackFinished()
    {
        while (_audioSource.isPlaying && !_isPaused) // Check if the music is not paused
        {
            yield return null;
        }

        if (!_isPaused) // Only call NextClip if the music is not paused
        {
            Debug.Log("Track finished");
            NextClip();
        }
    }

    public void Play()
    {
        if (!_audioSource.isPlaying)
        {
            _isPaused = false;
            _audioSource.Play();
            StartCoroutine(CheckIfTrackFinished());
        }
    }

    public void Pause()
    {
        if (_audioSource.isPlaying)
        {
            _isPaused = true; // Set the flag to true when pausing
            _audioSource.Pause();
            StopCoroutine(CheckIfTrackFinished()); // Stop the coroutine to prevent NextClip from being called
        }
    }

    public void NextClip()
    {
        Debug.Log("NextClip called"); // Debug statement
        _currentClipIndex = _currentClipIndex + 1;
        if (_currentClipIndex == audioClips.Count)
        {
            _currentClipIndex = 0;
        }
        _audioSource.clip = audioClips[_currentClipIndex];
        _audioSource.Play();
        _isPaused = false; // Reset the flag when playing a new clip
        StartCoroutine(CheckIfTrackFinished());
    }

}
