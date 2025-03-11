using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class AC_RadioShuffle : MonoBehaviour
{
    private AudioSource radioAudio;
    public List<AudioClip> songs; // List of audio clips
    public XRGrabInteractable grabInteractable; // VR interaction component

    void Start()
    {
        radioAudio = GetComponent<AudioSource>();

        // Make sure we have at least one song
        if (songs.Count > 0)
        {
            PlayRandomSong();
        }

        // Listen for user input (if you want a button to change songs)
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(SkipSong);
    }

    void Update()
    {
        // Check if the song finished playing
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
    }

    void SkipSong(ActivateEventArgs args) // If the player interacts with the radio
    {
        PlayRandomSong();
    }
}
