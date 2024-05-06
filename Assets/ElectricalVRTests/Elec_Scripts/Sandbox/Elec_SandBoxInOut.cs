using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SandBoxInOut : MonoBehaviour , Elec_IVoltage
{
    XRBaseInteractor interactor;
    public InOrOut orOut;
    public Elec_SandBoxItem item;
    Elec_IVoltage connectedEnd;
    public bool GiveOut = true;
    private void Start()
    {
        GiveOut = true;
        item = GetComponentInParent<Elec_SandBoxItem>();
        interactor = GetComponent<XRBaseInteractor>();
        interactor.selectEntered.AddListener(CheckInOrOut);
        interactor.selectExited.AddListener(DisconnectedWIre);
    }
    private void Update()
    {
        if (connectedEnd != null && orOut == InOrOut.OUT) 
        {
            if (GiveOut)
            {
                connectedEnd.Voltage_Receive(item.Voltage);
            }
            else if (!GiveOut) connectedEnd.Voltage_Receive(0);
        }
        else if(connectedEnd != null && orOut != InOrOut.OUT) 
        {
            item.Voltage = connectedEnd.Voltage_Send();
        }
    }
    void CheckInOrOut(SelectEnterEventArgs e)
    {
        connectedEnd = e.interactableObject.transform.GetComponent<Elec_IVoltage>(); 
        switch (orOut)
        {
            case InOrOut.OUT:
                if (GiveOut) e.interactableObject.transform.GetComponent<Elec_SandBoxWireEnd>().Voltage_Receive(item.Voltage);
                else if (!GiveOut)
                {
                    e.interactableObject.transform.GetComponent<Elec_SandBoxWireEnd>().Voltage_Receive(0);
                }
                break;
            case InOrOut.IN:
                item.Voltage = e.interactableObject.transform.GetComponent<Elec_SandBoxWireEnd>().Voltage_Send();               
                break;
        }
    }
    void DisconnectedWIre(SelectExitEventArgs e)
    {
        connectedEnd = null;
        switch (orOut)
        {
                case InOrOut.OUT:
                    e.interactableObject.transform.GetComponent<Elec_SandBoxWireEnd>().Voltage_Receive(0);
                break;
            case InOrOut.IN:
                e.interactableObject.transform.GetComponent<Elec_SandBoxWireEnd>().Voltage_Send();
                item.Voltage = 0;
                break;
        }
    }
    public void Voltage_Receive(int newVoltage)
    {
        throw new System.NotImplementedException();
    }

    public int Voltage_Send()
    {
        throw new System.NotImplementedException();
    }   
    public enum InOrOut
    {
        IN,
        OUT
    }
}
