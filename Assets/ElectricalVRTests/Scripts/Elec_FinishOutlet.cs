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
    public Elec_Multimeter multimeter;

    public int goalVoltage = 5;

    private void Start()
    {
        multimeter = GameObject.FindObjectOfType<Elec_Multimeter>();
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
    public void OnTriggerStay(Collider other)
    {   
        if(other.gameObject.GetComponent<Elec_Multimeter>() != null) 
        {         
            multimeter.VoltageMusltimeter = goalVoltage;
        }
        else if (other.tag == "StickyMultiMeter")
        {
            if (multimeter != null) { multimeter.StickyVoltage = goalVoltage; }
        }
    }
    public void OnTriggerExit(Collider other)
    {
       if (other.gameObject.GetComponent<Elec_Multimeter>() != null)
        {
            multimeter.VoltageMusltimeter = 0;
        }
        else if (other.tag == "StickyMultiMeter")
        {
            multimeter.StickyVoltage = 0;
        }
    }
}
