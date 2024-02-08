using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_BLmain : MonoBehaviour
{
    XRBaseInteractable interactable;
    XRBaseInteractor hand;
    Elec_BLCylinder Cylinders;
    [Obsolete]
    private void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        Cylinders = GetComponentInChildren<Elec_BLCylinder>();
        interactable.onSelectEntered.AddListener(HandIdentify);
        interactable.onSelectExited.AddListener(HandDelete);
    }
    public void Shoot()
    {        
        Cylinders.Cylinders[Cylinders.CurrenCylinderId].Propell();
        Cylinders.RotateToNExtRound();
    }

    private void HandDelete(XRBaseInteractor arg0)
    {
       hand= null;
    }

    private void HandIdentify(XRBaseInteractor arg0)
    {
        hand = arg0;
    }
}
