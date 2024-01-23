using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elec_Light_switch : MonoBehaviour
{
    public GameObject on;
    public GameObject off;
    bool ison = false;
    public UnityEvent WhatToWhenON,WhatToWhenOFF;
    public AudioClip ClickSound;
    AudioSource Audio;
    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
            if (other.tag == "LeftHand" || other.tag == "RightHand")
            {
                Audio.PlayOneShot(ClickSound);          
                if (!ison)
                {
                    WhatToWhenON.Invoke();          
                    off.SetActive(false);
                    on.SetActive(true);
                    ison = true;
                }
                else
                {
                    WhatToWhenOFF.Invoke();
                    off.SetActive(true);
                    on.SetActive(false);
                    ison = false;
                }
            }
    }
}
