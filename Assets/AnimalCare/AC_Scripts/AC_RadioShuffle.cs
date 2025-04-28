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

        // Listen for trigger press (activated)
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
        if (songs.Count == 0) return;

        AudioClip newSong;
        do
        {
            newSong = songs[Random.Range(0, songs.Count)];
        } while (newSong == radioAudio.clip && songs.Count > 1);

        radioAudio.clip = newSong;
        radioAudio.Play();
        Debug.Log("Audio is playing");
    }

    // Press trigger once while holding = mute/unmute
    public void OnTriggerPressed(ActivateEventArgs args)
    {
        radioAudio.mute = !radioAudio.mute; // Toggle mute/unmute
        Debug.Log("Trigger pressed. Radio muted: " + radioAudio.mute);
    }
}
