using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_Televisio : MonoBehaviour
{
    VideoPlayer player;
    public List<VideoClip> clipList;
    public VideoClip Static,Broken;
    public int channelID = 0;
    public bool PluggedIn = false;
    public float staticShowsFor = 0.1f;
    private bool ChangingClip = false;
    bool brokey = false;
    public AudioClip Break;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.SetTargetAudioSource(0,GetComponent<AudioSource>());
    }
    public void PluggedIN()
    {
        if (!brokey)
        {   
            player.isLooping = true;
            PluggedIn=true;
            player.clip= Static;
        }
    }
    public void Unplugged()
    {
        if (!brokey)
        {
            player.clip = null;
            player.isLooping = false;
            PluggedIn = false;
        }
    }
    public void SwitchChannel()
    {
        
        if (PluggedIn && !brokey)
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && !brokey)
        {
            player.clip = Broken;
            player.targetMaterialRenderer.gameObject.SetActive(true);
            brokey = true;
            GetComponent<AudioSource>().PlayOneShot(Break);
        }
    }
}
