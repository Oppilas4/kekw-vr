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
    public UnityEvent OnFinish;
    private bool hasFinished = false;
    XRBaseInteractor interactor;
    public Elec_GridNode ourGridNode;
    public Elec_Multimeter multimeter;

    public int goalVoltage = 5;
    public bool GoalReached = false;
    [Obsolete]
    private void Start()
    {
        multimeter = GameObject.FindObjectOfType<Elec_Multimeter>();
        ourGridNode= GetComponent<Elec_GridNode>();
        interactor = GetComponent<XRBaseInteractor>();
        interactor.onSelectEntered.AddListener(ReceiveVoltageFromCable);
        interactor.onSelectExited.AddListener(UnconnectedWire);
    }

    private void Update()
    {
        if (ourGridNode)
        {
            if (hasFinished == false)
            {
                if (ourGridNode.currentVoltage == goalVoltage && GoalReached)
                {
                    OnFinish.Invoke();
                    hasFinished = true;
                }
                else if (GoalReached)
                {
                    OnFinish.Invoke();
                    hasFinished = true;
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
    void ReceiveVoltageFromCable(XRBaseInteractable Staple)
    {
        if (Staple.GetComponent<Elec_StapleMakeStick>().SpoolItIsON.Voltage_Send() == goalVoltage)
        {
            GoalReached = true;
            Staple.GetComponent<Elec_StapleMakeStick>().SpoolItIsON.DisableWireSafely();
            LineRenderer temp = Staple.GetComponent<Elec_StapleMakeStick>().SpoolItIsON.GetComponent<LineRenderer>();
            temp.SetPosition(temp.positionCount - 1,interactor.attachTransform.transform.position);
        }   
    }
    void UnconnectedWire(XRBaseInteractable Staple)
    {
        GoalReached = false;
    }

}
