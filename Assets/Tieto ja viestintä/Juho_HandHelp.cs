using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Obsolete]
public class Juho_HandHelp : MonoBehaviour
{
    private XRGrabInteractable grabInteractor => GetComponent<XRGrabInteractable>();

    // Public variables for attach points
    public Transform leftHandAttachPoint;
    public Transform rightHandAttachPoint;

    private void OnEnable()
    {
        grabInteractor.selectEntered.AddListener(GrabbedBy);
    }

    private void OnDisable()
    {
        grabInteractor.selectEntered.RemoveListener(GrabbedBy);
    }

    private void GrabbedBy(SelectEnterEventArgs args)
    {
        // Check the tag of the interactor's GameObject to determine the hand
        string handTag = args.interactor.gameObject.tag;
        if (handTag == "LeftHand")
        {
            grabInteractor.attachTransform = leftHandAttachPoint;
        }
        else if (handTag == "RightHand")
        {
            grabInteractor.attachTransform = rightHandAttachPoint;
        }
    }
}
