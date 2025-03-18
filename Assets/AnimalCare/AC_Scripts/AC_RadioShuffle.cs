using UnityEngine;
using System.Collections.Generic;

public class AC_RadioShuffle : MonoBehaviour
{
    private AudioSource radioAudio;
    public List<AudioClip> songs; // List of audio clips

    void Start()
    {
        radioAudio = GetComponent<AudioSource>();

        // Ensure we have at least one song
        if (songs.Count > 0)
        {
            PlayRandomSong();
        }
    }

    void Update()
    {
        // Check if the song finished playing and play a random song
        if (!radioAudio.isPlaying && radioAudio.clip != null)
        {
            PlayRandomSong();
        }
    }

    void PlayRandomSong()
    {
        if (songs.Count == 0) return; // Safety check

        // Pick a random song that isn't the current one
        AudioClip newSong;
        do
        {
            newSong = songs[Random.Range(0, songs.Count)];
        } while (newSong == radioAudio.clip && songs.Count > 1);

        // Play the selected song
        radioAudio.clip = newSong;
        radioAudio.Play();
        Debug.Log("Audio is playing");
    }
}
