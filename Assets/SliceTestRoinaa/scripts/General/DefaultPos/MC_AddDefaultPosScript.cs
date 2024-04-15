using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MC_AddDefaultPosScript : MonoBehaviour
{
    void Start()
    {
        // Find all objects with the XRGrabInteractable component
        XRGrabInteractable[] grabInteractables = FindObjectsOfType<XRGrabInteractable>();

        // Iterate over each object and add the GrabbableObject component
        foreach (XRGrabInteractable grabInteractable in grabInteractables)
        {
            // Check if the object already has a GrabbableObject component to avoid duplicates
            if (grabInteractable.GetComponent<MC_MoveToDefaultPosition>() == null)
            {
                // Add the GrabbableObject component
                grabInteractable.gameObject.AddComponent<MC_MoveToDefaultPosition>();
            }
        }
    }
}
