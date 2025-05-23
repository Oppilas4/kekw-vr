using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class AC_RadioShuffle : MonoBehaviour
{
    private AudioSource radioAudio;
    private XRGrabInteractable grabInteractable;

    public List<AudioClip> songs; // List of songs

    private void Start()
    {
        radioAudio = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Listen for the trigger press (activated)
        grabInteractable.activated.AddListener(OnTriggerPressed);

        if (songs.Count > 0)
        {
            PlayRandomSong();
        }
    }

    private void OnDestroy()
    {
        grabInteractable.activated.RemoveListener(OnTriggerPressed);
    }

    void PlayRandomSong()
    {
        if (songs.Count == 0) return; // Safety check to ensure we have at least one song

        // Pick a random song
        AudioClip newSong;
        do
        {
            newSong = songs[Random.Range(0, songs.Count)];
        } while (newSong == radioAudio.clip && songs.Count > 1); // Ensure the song isn't the same as the current one

        // Set the new song and play it
        radioAudio.clip = newSong;
        radioAudio.Play();
    }

    // Called when the trigger is pressed while holding the object
    public void OnTriggerPressed(ActivateEventArgs args)
    {
        if (radioAudio.mute)
        {
            // If it was muted, unmute and play a random song
            radioAudio.mute = false;

            // Pick and play a random song
            PlayRandomSong();
        }
        else
        {
            // If it's not muted, mute the audio
            radioAudio.mute = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current song has finished playing and it's not muted
        if (!radioAudio.isPlaying && radioAudio.clip != null && !radioAudio.mute)
        {
            // Play a new random song when the current one finishes
            PlayRandomSong();
        }
    }
}
