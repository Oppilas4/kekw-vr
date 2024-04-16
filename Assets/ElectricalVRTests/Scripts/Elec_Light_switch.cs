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
    public bool reset;
    public Hand WhichHand;
    Input TriggerInput;
    private void Start()
    {
        boxItem = GetComponent<Elec_SandBoxItem>();
        Audio = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CapsuleCollider>() != null && other.CompareTag("RightHand") || other.GetComponent<CapsuleCollider>() != null && other.CompareTag("LeftHand"))
                {
                    if(other.CompareTag("RightHand"))WhichHand = Hand.RightHand;
                    else if(other.CompareTag("LeftHand"))WhichHand= Hand.LeftHand;
                    Audio.PlayOneShot(ClickSound);                             
                }                
    }
    private void OnTriggerExit(Collider other)
    {
        WhichHand = Hand.none;
    }
    private void Update()
    {
            switch (WhichHand)
            {
                case Hand.RightHand:
                    if (Input.GetButtonDown("XRI_Right_TriggerButton") && reset)
                    {
                        Debug.Log("Plop");
                        reset = false;
                        CallOnOff();
                    }
                    else
                    {
                        reset = true;
                    }
                break;
                case Hand.LeftHand:
                    if (Input.GetButtonDown("XRI_Left_TriggerButton") && reset)
                    {
                        Debug.Log("Plop");
                        reset = false;
                        CallOnOff();
                    }
                    else
                    {
                        reset = true;
                    }
                    break;
        }
    }
    [ContextMenu("OnOff")]
    public void CallOnOff()
    {
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
    public enum Hand
    {
        LeftHand,
        RightHand,
        none
    }
}
