using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Elec_FinishOutlet : MonoBehaviour
{
    public UnityEvent OnFinish;
    private bool hasFinished = false;

    public Elec_GridNode ourGridNode;
    public Elec_WireEnds wireEnd;
    public Elec_Multimeter multimeter;
    public Elev_MultimeterSticky multimeterSticky;

    public int goalVoltage = 5;

    private void Start()
    {
        ourGridNode= GetComponent<Elec_GridNode>();
    }

    private void Update()
    {
        if (ourGridNode)
        {
            if (hasFinished == false)
            {
                if (ourGridNode.currentVoltage == goalVoltage)
                {
                    if (GetComponent<XRSocketInteractor>().hasSelection == true)
                    {
                        OnFinish.Invoke();
                        hasFinished = true;
                    }
                }
            }
            else
            {
                if (ourGridNode.currentVoltage != goalVoltage) hasFinished = false;
            }
        }
    }

    public void WireConnected()
    {
        Debug.Log("WireConnected called");
        if (wireEnd.WireEndVolt.voltage == ourGridNode.ourVoltage.voltage)
        {
            //OnFinish.BulbEnablee();
            Debug.Log("Bulb was lit on");
        }
    }
    public void WireDisconnected()
    {
        wireEnd = null;
       // OnFinish.BulbDisable();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Elec_WireEnds>() != null) 
        {
            wireEnd = other.gameObject.GetComponent<Elec_WireEnds>();
        }
        else if(other.gameObject.GetComponent<Elec_Multimeter>() != null) 
        {
            multimeter = other.gameObject.GetComponent<Elec_Multimeter>();
            multimeter.VoltageMusltimeter = ourGridNode.ourVoltage.voltage;
        }
        else if (other.gameObject.GetComponent<Elev_MultimeterSticky>() != null)
        {
            multimeterSticky = other.gameObject.GetComponent <Elev_MultimeterSticky>();
            multimeter.StickyVoltage = ourGridNode.ourVoltage.voltage;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Elec_WireEnds>() != null)
        {
            wireEnd = null;
        }
        else if (other.gameObject.GetComponent<Elec_Multimeter>() != null)
        {
            multimeter.VoltageMusltimeter = 0;
        }
        else if (other.gameObject.GetComponent<Elev_MultimeterSticky>() != null)
        {
            multimeter.StickyVoltage = 0;
        }
    }
}
