using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_Light_switch : MonoBehaviour
{
    public GameObject on;
    public GameObject off;
    bool ison = false;
    public UnityEvent WhatToWhenON,WhatToWhenOFF;
    public AudioClip ClickSound;
    AudioSource Audio;
    public bool Sandbox = false;
    Elec_SandBoxItem boxItem;
    int SavedVoltage;
    public Elec_SandBoxInOut OutPut;
    private void Start()
    {
        boxItem = GetComponent<Elec_SandBoxItem>();
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
    public void OnOff()
    {
        if (boxItem.Voltage > 0)
        {
            OutPut.GiveOut = false;
            Debug.Log("BallsOut");
            SavedVoltage = boxItem.Voltage;
        }
        else if(boxItem.Voltage <= 0) 
        {
            OutPut.GiveOut = true;
            boxItem.Voltage = SavedVoltage;
        }
    }
}
