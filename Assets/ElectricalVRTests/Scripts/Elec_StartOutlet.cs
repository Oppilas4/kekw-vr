using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class Elec_StartOutlet : MonoBehaviour
{
    public Elec_GridNode ourNode;
    public Elec_WireEnds wireEnd;
    public Wire MainWire;
    public Elec_Multimeter multimeter;
    public Elev_MultimeterSticky multimeterSticky;
    private void Start()
    {
        ourNode = GetComponent<Elec_GridNode>();
    }
    public void WireConnected()
    {
        Debug.Log("WireConnected to start called");
        if (MainWire == null) return;
        MainWire = wireEnd.MainestWire;
        MainWire.WireVoltage.voltage = ourNode.ourVoltage.voltage;
    }
    public void WireDisconnected()
    {
        if(wireEnd) wireEnd.WireEndVolt.voltage = 0;
        if(MainWire) MainWire.WireVoltage.voltage = 0;
        MainWire = null;
        wireEnd = null;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Elec_WireEnds>() != null) 
        {
            wireEnd = other.gameObject.GetComponent<Elec_WireEnds>();
        }
        else if (other.gameObject.GetComponent<Elec_Multimeter>() != null)
        {
            multimeter = other.gameObject.GetComponent<Elec_Multimeter>();
            multimeter.VoltageMusltimeter = ourNode.ourVoltage.voltage;
        }
        else if (other.gameObject.GetComponent<Elev_MultimeterSticky>() != null)
        {
            multimeterSticky = other.gameObject.GetComponent<Elev_MultimeterSticky>();
            multimeter.StickyVoltage = ourNode.ourVoltage.voltage;
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
