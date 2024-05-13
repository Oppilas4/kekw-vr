using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Obsolete]
public class MC_AdjustAttachTransform : MonoBehaviour
{
    public Transform rightHandAttachTransform;
    public Transform leftHandAttachTransform;

    public XRGrabInteractable grabInteractable;
    public XRSocketInteractor socketInteractor;

    void Start()
    {
        if (socketInteractor == null)
        {
            socketInteractor = GetComponentInChildren<XRSocketInteractor>();
        }
        // Subscribe to the OnHoverEntered event
        socketInteractor.onHoverEnter.AddListener(HandleHoverEnter);
        // Subscribe to the OnHoverExited event
        socketInteractor.onHoverExit.AddListener(HandleHoverExit);
    }

    void HandleHoverEnter(XRBaseInteractable interactable)
    {
        if (interactable == grabInteractable)
        {
            // Change the socket attach point based on the hand name
            string handName = grabInteractable.attachTransform.name;
            socketInteractor.attachTransform = handName == "LHand" ? leftHandAttachTransform : rightHandAttachTransform;
        }
    }

    void HandleHoverExit(XRBaseInteractable interactable)
    {
        /*
        if (interactable == grabInteractable)
        {
            // Reset the socket attach point or set to a default value if needed
            socketInteractor.attachTransform = null; // Or set to a default Transform
        }
        */
    }

    void OnDestroy()
    {
        // Unsubscribe from the events when the object is destroyed
        socketInteractor.onHoverEnter.RemoveListener(HandleHoverEnter);
        socketInteractor.onHoverExit.RemoveListener(HandleHoverExit);
    }
}
