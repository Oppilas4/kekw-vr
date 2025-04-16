using UnityEngine.XR.Interaction.Toolkit;

// Inside your class:

void ClampVacuumHeadPosition()
{
    Vector3 toHead = vacuumHead.position - hoseOrigin;
    float distance = toHead.magnitude;

    if (distance > maxHoseLength)
    {
        // Try to force drop via XR
        XRGrabInteractable grabInteractable = vacuumHead.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            // Get the interactor holding the object
            IXRSelectInteractor interactor = grabInteractable.firstInteractorSelecting;
            if (interactor != null)
            {
                XRInteractionManager interactionManager = grabInteractable.interactionManager;

                if (interactionManager != null)
                {
                    interactionManager.SelectExit(interactor, grabInteractable);
                    Debug.Log("Hose was force-dropped due to overstretch!");
                }
            }
        }

        // Optionally destroy the joint or disable physics interactions
        ConfigurableJoint endJoint = segments[^1].GetComponent<ConfigurableJoint>();
        if (endJoint != null)
        {
            Destroy(endJoint);
        }

        // Add impulse for drama
        Rigidbody lastRb = segments[^1].GetComponent<Rigidbody>();
        if (lastRb != null)
        {
            Vector3 snapDir = toHead.normalized;
            lastRb.AddForce(-snapDir * 5f, ForceMode.Impulse);
        }

        // Optional: disable further updates
        enabled = false;
    }
}
