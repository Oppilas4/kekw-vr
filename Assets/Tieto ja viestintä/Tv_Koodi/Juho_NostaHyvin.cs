using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Obsolete]
public class Juho_NostaHyvin : XRRayInteractor
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Get grab interactable from prefab
        XRGrabInteractable objectInteractable = args.interactor.GetComponent<XRGrabInteractable>();

        // Select object into the same interactor (hand)
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);

        base.OnSelectEntered(args);
    }
}
