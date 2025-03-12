using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class AC_RadioShuffle : MonoBehaviour
{
    private AudioSource radioAudio;
    public List<AudioClip> songs; // List of audio clips

    private XRNode inputSource = XRNode.RightHand; // You can change this to LeftHand if needed
    private InputDevice device;

    void Start()
    {
        radioAudio = GetComponent<AudioSource>();

        // Ensure we have at least one song
        if (songs.Count > 0)
        {
            PlayRandomSong();
        }

        // Get the input device for the selected XRNode (Right Hand by default)
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    void Update()
    {
        // Check if the input device is valid
        if (!device.isValid)
        {
            device = InputDevices.GetDeviceAtXRNode(inputSource); // Re-check the device
        }

        // Listen for the trigger press on the controller
        bool triggerPressed;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
        {
            StopMusic(); // Stop the music when the trigger is pressed
        }

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
    }

    // This method stops the music when the trigger is pressed
    void StopMusic()
    {
        if (radioAudio.isPlaying)
        {
            radioAudio.Stop();
        }
    }
}
