using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Elec_Televisio : MonoBehaviour
{
    VideoPlayer player;
    public List<VideoClip> clipList;
    public VideoClip Static, NananaNA;
    public int channelID = 0;
    public bool PluggedIn = false;
    public float staticShowsFor = 0.1f;
    private bool ChangingClip = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.SetTargetAudioSource(0,GetComponent<AudioSource>());
    }
    public void PluggedIN()
    {
        player.isLooping = false;
        player.enabled = true;
        PluggedIn=true;
        player.clip= Static;
    }
    public void Unplugged()
    {
        player.clip = NananaNA;
        player.isLooping = false;
        PluggedIn = false;    
    }
    public void SwitchChannel()
    {
        
        if (PluggedIn)
        {
            if (channelID >= clipList.Count)
            {
                channelID = 0;
                return;
            }
            if(ChangingClip) StopCoroutine(ChangeClip(clipList[channelID]));
            StartCoroutine(ChangeClip(clipList[channelID]));
            
        }     
    }

    IEnumerator ChangeClip(VideoClip nextClip)
    {
        ChangingClip = true;
        player.clip= Static;
        yield return new WaitForSeconds(staticShowsFor);
        player.clip = clipList[channelID];
        ChangingClip = false;
        channelID++;
    }
}
