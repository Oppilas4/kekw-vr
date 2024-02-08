using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class MC_tongsController : MonoBehaviour
{
    public InputActionProperty rightTriggerValue;
    public InputActionProperty leftTriggerValue;
    private Animator tongsAnimator;
    public float grabDistance = 0.11f; // Adjust the distance to your needs
    private XRGrabInteractable grabbedObject;
    public Transform grabLocation;

    private XRGrabInteractable grabInteractor => GetComponent<XRGrabInteractable>();
    private bool isGrabbed = false;

    void OnEnable()
    {
        grabInteractor.selectEntered.AddListener(OnGrabStart);
        grabInteractor.selectExited.AddListener(OnGrabEnd);
    }

    void OnDisable()
    {
        grabInteractor.selectEntered.RemoveListener(OnGrabStart);
        grabInteractor.selectExited.RemoveListener(OnGrabEnd);
    }

    // Callback when the tongs are grabbed
    private void OnGrabStart(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        // Perform any setup needed when the tongs are grabbed
    }

    // Callback when the tongs are released
    private void OnGrabEnd(SelectExitEventArgs args)
    {
        isGrabbed = false;

        // Check if there is an object currently being held
        if (grabbedObject != null)
        {
            // Release the object by setting its parent to null and resetting its Rigidbody
            ReleaseObject();
            tongsAnimator.SetFloat("Close", 0f);
        }
    }

    void Start()
    {
        tongsAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the tongs are being grabbed and either the right or left triggerValue property is valid
        if (isGrabbed)
        {
            float triggerAmount = 0f;

            var interactor = grabInteractor.interactorsSelecting[0];

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

            if (grabbedObject != null)
            {
                if (triggerAmount <= 0.5f)
                {
                    tongsAnimator.SetFloat("Close", triggerAmount);
                }
            }
            else
            {
                // Set the "Close" parameter of the Animator based on the trigger input
                tongsAnimator.SetFloat("Close", triggerAmount);
            }

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
            if (Physics.Raycast(grabLocation.position, transform.up, out hit, grabDistance))
            {
                XRGrabInteractable interactable = hit.collider.GetComponent<XRGrabInteractable>();
                if (interactable != null)
                {
                    // Grab the interactable using the XRGrabInteractable's Grab method
                    grabbedObject = interactable;

                    // Get the Rigidbody of the grabbed object
                    Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        // Set the Rigidbody to kinematic to prevent physics interactions while grabbed
                        rb.isKinematic = true;
                    }

                    // Parent the grabbed object to the tongs
                    grabbedObject.transform.parent = transform;
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

            // Reset the rigidbody's velocity to zero
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
            }

            grabbedObject = null;
        }
    }

    // Visualize the raycast in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(grabLocation.position, transform.up * grabDistance);
    }
}
