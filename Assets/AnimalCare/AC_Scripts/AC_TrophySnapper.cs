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
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Lock it in place
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

            // Snap to this transform
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

#if ENABLE_INPUT_SYSTEM
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (disableGrabbingAfterSnap && grab != null)
            {
                grab.enabled = false;
            }
#endif

            hasSnapped = true;
        }
    }
}
