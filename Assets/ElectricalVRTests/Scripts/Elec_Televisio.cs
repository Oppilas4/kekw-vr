using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Elec_Televisio : MonoBehaviour
{
    VideoPlayer player;
    public List<VideoClip> clipList;
    public VideoClip Static;
    AudioSource audioSource;
    public int channelID = 0;
    public bool PluggedIn = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<VideoPlayer>();
        player.SetTargetAudioSource(0,audioSource);
        
    }
    public void PluggedIN()
    {
        player.enabled = true;
        PluggedIn=true;
        player.clip= Static;
    }
    public void Unplugged()
    {
        player.enabled = false;
        PluggedIn = false;
    }
    public void SwitchChannel()
    {
        
        if (PluggedIn)
        {
            if (channelID >= clipList.Count)
            {
                channelID = 0;
            }
            player.clip = clipList[channelID];
            channelID++;
        }     
    }
}
