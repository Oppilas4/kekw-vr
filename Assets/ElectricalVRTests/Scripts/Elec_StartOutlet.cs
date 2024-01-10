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
    public int OutputVoltage = 5;
    public Elec_WireEnds wireEnd;
    public Wire MainWire;
    public Elec_Multimeter multimeter;
    public Elev_MultimeterSticky multimeterSticky;
    private void Start()
    {
    }
    public void WireConnected()
    {
        Debug.Log("WireConnected to start called");
        MainWire = wireEnd.MainestWire;
        MainWire.WireVoltage = OutputVoltage;
    }
    public void WireDisconnected()
    {
        wireEnd.WireEndVolt = 0;
        MainWire.WireVoltage = 0;
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
            multimeter.VoltageMusltimeter = OutputVoltage;
        }
        else if (other.gameObject.GetComponent<Elev_MultimeterSticky>() != null)
        {
            multimeterSticky = other.gameObject.GetComponent<Elev_MultimeterSticky>();
            multimeterSticky.EqualVoltages(OutputVoltage);
        }
    }
    public void OnTriggerExit(Collider other)
    {
            wireEnd = null;
            multimeter.VoltageMusltimeter = 0;
    }
}
