using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class Elec_FinishOutlet : MonoBehaviour
{
    public Elec_LightBulb bulb;
    public Elec_GridNode ourGridNode;
    public Elec_WireEnds wireEnd;
    public Elec_Multimeter multimeter;
    public Elev_MultimeterSticky multimeterSticky;
    private void Start()
    {
        ourGridNode= GetComponent<Elec_GridNode>();
    }
    public void WireConnected()
    {
        Debug.Log("WireConnected called");
        if (wireEnd.WireEndVolt.voltage == ourGridNode.ourVoltage.voltage)
        {
            bulb.BulbEnablee();
            Debug.Log("Bulb was lit on");
        }
    }
    public void WireDisconnected()
    {
        wireEnd = null;
        bulb.BulbDisable();
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
            multimeterSticky.Chill();
            multimeter.StickyVoltage = 0;
        }
    }
}
