using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class MC_tongsController : MonoBehaviour
{
    private XRGrabInteractable grabbable;
    public InputActionProperty rightTriggerValue;
    public InputActionProperty leftTriggerValue;
    private Animator tongsAnimator;
    public float grabDistance = 0.1f; // Adjust the distance to your needs
    private XRGrabInteractable grabbedObject;
    public Transform grabLocation;
    void Start()
    {
        grabbable = GetComponent<XRGrabInteractable>();
        tongsAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the tongs are being grabbed and either the right or left triggerValue property is valid
        if (grabbable.isSelected)
        {
            float triggerAmount = 0f;

            var interactor = grabbable.interactorsSelecting[0];

            if (interactor != null)
            {
                // Check the tag of the interactor's game object
                if (interactor.transform.gameObject.tag == "RightHand" && rightTriggerValue != null && rightTriggerValue.action != null)
                {
                    // Check if the right-hand trigger is pressed
                    triggerAmount = rightTriggerValue.action.ReadValue<float>();
                }
                else if (interactor.transform.gameObject.tag == "LeftHand" && leftTriggerValue != null && leftTriggerValue.action != null)
                {
                    // Check if the left-hand trigger is pressed
                    triggerAmount = leftTriggerValue.action.ReadValue<float>();
                }
            }

            // Set the "Close" parameter of the Animator based on the trigger input
            tongsAnimator.SetFloat("Close", triggerAmount);

            // Check for grabbing objects
            if (triggerAmount > 0f)
            {
                GrabObject();
            }
            else
            {
                // Release the grabbed object if trigger is not pressed
                ReleaseObject();
            }
        }
    }

    void GrabObject()
    {
        if (grabbedObject == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(grabLocation.position, transform.forward, out hit, grabDistance))
            {
                XRGrabInteractable interactable = hit.collider.GetComponent<XRGrabInteractable>();
                if (interactable != null)
                {
                    // Grab the interactable using the XRGrabInteractable's Grab method
                    grabbedObject = interactable;

                    // Set the attach point to the grabLocation.position
                    grabbedObject.attachTransform = grabLocation;

                    // Set the object as a child of the grab location
                    grabbedObject.transform.parent = grabLocation;

                    
                    
                }
            }
        }
    }


    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            // Reset the parent of the grabbed object.
            grabbedObject.transform.parent = null;
            

            grabbedObject = null;
        }
    }




    // Visualize the raycast in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(grabLocation.position, transform.forward * grabDistance);
    }
}
