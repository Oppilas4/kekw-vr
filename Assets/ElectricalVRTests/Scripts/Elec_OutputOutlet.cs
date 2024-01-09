using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class Elec_OutputOutlet : MonoBehaviour
{
    public Elec_LightBulb bulb;
    public int OutputVoltage = 5;
    Elec_WireEnds wireEnd;
    XRSocketInteractor interactor;
    public void WireConnected(XRBaseInteractable interactable)
    {  
        wireEnd = interactable.GetComponent<Elec_WireEnds>();
        if (wireEnd.WireEndVolt == OutputVoltage)
        {
            bulb.PuzzleComplete();
        }
    }
    [Obsolete]
    void Start()
    {
        interactor = GetComponent<XRSocketInteractor>();
        interactor.onSelectEntered.AddListener(WireConnected);
    }
}
