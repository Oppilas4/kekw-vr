using UnityEngine;

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
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Snap to THIS object's position and rotation
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

#if ENABLE_INPUT_SYSTEM
            var grab = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
            if (disableGrabbingAfterSnap && grab != null)
            {
                grab.enabled = false;
            }
#endif

            hasSnapped = true;
        }
    }
}
