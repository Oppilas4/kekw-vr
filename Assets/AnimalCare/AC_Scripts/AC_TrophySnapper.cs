using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.XR.Interaction.Toolkit;
#endif

public class AC_TrophySnapper : MonoBehaviour
{
    public string trophyTag = "Trophy";
    public bool disableGrabbingAfterSnap = true;

    private bool hasSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasSnapped) return;

        if (other.CompareTag(trophyTag))
        {
#if ENABLE_INPUT_SYSTEM
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null && grab.isSelected)
            {
                // Force it out of the player's hand
                grab.interactionManager.SelectExit(grab.interactorsSelecting[0], grab);
            }
#endif

            // Snap position and rotation
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

#if ENABLE_INPUT_SYSTEM
            if (disableGrabbingAfterSnap && grab != null)
            {
                grab.enabled = false;
            }
#endif

            hasSnapped = true;
        }
    }
}
