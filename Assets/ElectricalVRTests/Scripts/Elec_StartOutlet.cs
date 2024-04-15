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
    public Elec_Multimeter multimeter;
    private void Start()
    {
        multimeter = GameObject.FindObjectOfType<Elec_Multimeter>();
        ourNode = GetComponent<Elec_GridNode>();
    }
    public void OnTriggerStay(Collider other)
    {     
        if(ourNode.ElectricityIsOn) 
        {
            if (other.gameObject.GetComponent<Elec_Multimeter>() != null)
            {
                multimeter = other.GetComponent<Elec_Multimeter>();
                multimeter.VoltageMusltimeter = ourNode.ourVoltage.voltage;
            }
            else if (other.tag == "StickyMultiMeter")
            {
                multimeter = other.GetComponent<Elec_MultiStick>().MamaMultimeter;
                if (multimeter != null) { multimeter.StickyVoltage = ourNode.ourVoltage.voltage; }  
            }
        }

    }
    public void OnTriggerExit(Collider other)
    {      
         if (other.gameObject.GetComponent<Elec_Multimeter>() != null)
        {         
            multimeter.VoltageMusltimeter = 0;
            multimeter = null;
        }
        else if (other.tag == "StickyMultiMeter")
        {          
            multimeter.StickyVoltage = 0;
            multimeter = null;
        }
    }
}
