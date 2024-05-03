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
    public UnityEvent OnFinish,OnEnabled;
    private bool hasFinished = false;
    XRBaseInteractor interactor;
    public Elec_GridNode ourGridNode;
    public Elec_Multimeter multimeter;

    public int goalVoltage = 5;
    public bool GoalReached = false;
    [Obsolete]
    private void Start()
    {
        ourGridNode= GetComponent<Elec_GridNode>();
        interactor = GetComponent<XRBaseInteractor>();
        interactor.onSelectEntered.AddListener(ReceiveVoltageFromCable);
        interactor.onSelectExited.AddListener(UnconnectedWire);
    }

    private void Update()
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
    public void OnTriggerStay(Collider other)
    {   
        if (ourGridNode.ElectricityIsOn)
        {
            if(other.gameObject.GetComponent<Elec_Multimeter>() != null) 
            {
                multimeter = other.GetComponent<Elec_Multimeter>();
                multimeter.VoltageMusltimeter = goalVoltage;
            }
            else if (other.tag == "StickyMultiMeter")
            {
                multimeter = other.GetComponent<Elec_MultiStick>().MamaMultimeter;
                if (multimeter != null) { multimeter.StickyVoltage = goalVoltage; }
            }
        }
   
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Elec_Multimeter>() != null)
        {
            multimeter = other.GetComponent<Elec_Multimeter>();
            multimeter.VoltageMusltimeter = 0;
            multimeter = null;
        }
        else if (other.tag == "StickyMultiMeter")
        {
            multimeter = other.GetComponent<Elec_MultiStick>().MamaMultimeter;
            multimeter.StickyVoltage = 0;
            multimeter = null;
        }
    }   
    void ReceiveVoltageFromCable(XRBaseInteractable Staple)
    {
            GoalReached = true;
            Staple.GetComponent<Elec_StapleMakeStick>().SpoolItIsON.DisableWireSafely();
            LineRenderer temp = Staple.GetComponent<Elec_StapleMakeStick>().SpoolItIsON.GetComponent<LineRenderer>();
            temp.SetPosition(temp.positionCount - 1,interactor.attachTransform.transform.position);
            ourGridNode.ourManager.LinesCompleted++; 
    }
    void UnconnectedWire(XRBaseInteractable Staple)
    {
        GoalReached = false;
    }
    private void OnEnable()
    {
        OnEnabled.Invoke();
    }
}
