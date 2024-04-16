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
    public Elec_SandBoxInOut OutPut,Output2;
    public KindOfSwitch WhatKInd;
    public bool reset;
    public Hand WhichHand;
    Collider InCase;
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
       if(other == InCase) WhichHand = Hand.none;
    }
    private void Update()
    {
            switch (WhichHand)
            {
                case Hand.RightHand:
                    if (Input.GetButtonDown("XRI_Right_TriggerButton") && reset)
                    {
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
    [ContextMenu("OnOff")]
    public void OnOff()
    {
        Debug.Log("OnOff");
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
