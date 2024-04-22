using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_SandboxSpeaker : MonoBehaviour
{
    AudioSource audioSource;
    public List<AudioClip> Music;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    [ContextMenu("PLAY")]
    public void PlayMusic()
    {      
        audioSource.PlayOneShot(Music[Random.Range(0, Music.Count)]);
    }
}
