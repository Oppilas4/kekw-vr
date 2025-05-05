using UnityEngine;

public class AC_TrophySnapper : MonoBehaviour
{
    [Header("Assign the snap location (usually an empty GameObject)")]
    public Transform snapPoint;

    [Header("Tag used to identify the trophy object")]
    public string trophyTag = "Trophy";

    [Header("Should the trophy be parented to the snap point?")]
    public bool parentAfterSnap = true;

    [Header("Optional feedback")]
    public AudioSource snapSound;
    public bool disableGrabbingAfterSnap = true;

    private bool hasSnapped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasSnapped) return;

        if (other.CompareTag(trophyTag))
        {
            // Disable physics
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Align position and rotation
            other.transform.position = snapPoint.position;
            other.transform.rotation = snapPoint.rotation;

            // Optional parenting
            if (parentAfterSnap)
            {
                other.transform.SetParent(snapPoint);
            }

            // Disable XR Grab if using it
#if ENABLE_INPUT_SYSTEM
            var grab = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
            if (disableGrabbingAfterSnap && grab != null)
            {
                grab.enabled = false;
            }
#endif

            // Play sound if assigned
            if (snapSound != null)
            {
                snapSound.Play();
            }

            hasSnapped = true;
        }
    }
}
