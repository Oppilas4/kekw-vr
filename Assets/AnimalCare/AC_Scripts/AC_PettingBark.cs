using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_PettingBark : MonoBehaviour
{
    float cooldown = 2f;
    float lastPlayTime;
    public AudioSource myclip;
    public void PlaySound()
    {
        if (Time.time - lastPlayTime > cooldown)
        {
            myclip.Play();
            lastPlayTime = Time.time;
        }
    }
}
