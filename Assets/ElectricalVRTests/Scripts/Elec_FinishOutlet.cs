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
    public int OutputVoltage = 5;
    public Elec_WireEnds wireEnd;
    private void Start()
    {
    }
    public void WireConnected()
    {
        Debug.Log("WireConnected called");
        if (wireEnd.WireEndVolt == OutputVoltage)
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
    }
    public void OnTriggerExit(Collider other)
    {
            wireEnd = null;   
    }
}
