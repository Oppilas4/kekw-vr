using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MC_LimitDrawerMovement : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable drawerHandle;
    private bool isDrawnHandleGrabbed = false;
    public ConfigurableJoint drawerJoint;
    private XRBaseInteractor grabbingHand;
    private Vector3 initialHandPosition;
    private Vector3 lastPosition;

    void Start()
    {
        drawerHandle.selectEntered.AddListener(OnDrawnHandleGrabbed);
        drawerHandle.selectExited.AddListener(OnDrawnHandleReleased);
    }

    void OnDestroy()
    {
        drawerHandle.selectEntered.RemoveListener(OnDrawnHandleGrabbed);
        drawerHandle.selectExited.RemoveListener(OnDrawnHandleReleased);
    }

    void OnDrawnHandleGrabbed(SelectEnterEventArgs args)
    {
        isDrawnHandleGrabbed = true;
        grabbingHand = args.interactorObject as XRBaseInteractor;
        initialHandPosition = grabbingHand.transform.position;
        Debug.Log("Grabbed");
    }

    void OnDrawnHandleReleased(SelectExitEventArgs args)
    {
        isDrawnHandleGrabbed = false;
        grabbingHand = null;
        Debug.Log("release");
    }

    void Update()
    {
        if (isDrawnHandleGrabbed && grabbingHand != null)
        {
            float handDistanceFromInitialPosition = Vector3.Distance(initialHandPosition, grabbingHand.transform.position);
            float jointLimit = drawerJoint.linearLimit.limit;

            if (handDistanceFromInitialPosition > jointLimit)
            {
                Debug.Log("stop movement");
                // Stop updating the drawer's position
                transform.position = lastPosition;
            }
            else
            {
                // Update the drawer's position normally
                lastPosition = transform.position;
            }
        }
    }
}
