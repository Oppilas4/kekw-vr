using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_Light_switch : MonoBehaviour
{
    public GameObject on, off;
    bool ison = false;
    public UnityEvent WhatToWhenON,WhatToWhenOFF;
    public AudioClip ClickSound;
    AudioSource Audio;
    public bool Sandbox = false;
    Elec_SandBoxItem boxItem;
    int SavedVoltage;
    public Elec_SandBoxInOut OutPut,Output2;
    public KindOfSwitch WhatKInd;
    private void Start()
    {
        boxItem = GetComponent<Elec_SandBoxItem>();
        Audio = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CapsuleCollider>() != null && other.CompareTag("RightHand") || other.GetComponent<CapsuleCollider>() != null && other.CompareTag("LeftHand"))
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
        switch (WhatKInd)
        {
            case KindOfSwitch.ONOFF:
                if (OutPut.GiveOut) OutPut.GiveOut = false;
                else if (!OutPut.GiveOut) OutPut.GiveOut = true;
                break;
            case KindOfSwitch.OR:
                if (OutPut.GiveOut)
                {
                    OutPut.GiveOut = false;
                    Output2.GiveOut = true;
                }
                else if(!OutPut.GiveOut) 
                {
                    OutPut.GiveOut = true;
                    Output2.GiveOut = false;
                }
                break;
        }
        if (OutPut.GiveOut)OutPut.GiveOut = false;
        else if(!OutPut.GiveOut) OutPut.GiveOut = true;
    }
    public enum KindOfSwitch
    {
        ONOFF,
        OR
    }
}
