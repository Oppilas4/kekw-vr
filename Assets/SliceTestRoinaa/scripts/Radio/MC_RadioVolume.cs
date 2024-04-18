using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_RadioVolume : MonoBehaviour, IDial
{
    public AudioSource _audioSource;
    public void DialChanged(float dialValue)
    {
        Debug.Log(dialValue);
        // Normalize the dial value to a range of 0 to 1, where 360 corresponds to 1
        float normalizedValue = dialValue / 180f;

        // Clamp the normalized value between 0 and 1
        normalizedValue = Mathf.Clamp(normalizedValue, 0f, 1f);
        Debug.Log(normalizedValue);
        // Set the audio source volume based on the normalized value
        _audioSource.volume = normalizedValue;
    }
}
