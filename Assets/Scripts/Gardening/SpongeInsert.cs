using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Gardening
{
    /// <summary>
    /// Class that handles inserting flowers into floral sponges
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class SpongeInsert : MonoBehaviour
    {
        [SerializeField]
        private XRGrabInteractable _interactable;
        private bool _isAnchored = false;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _interactable = GetComponent<XRGrabInteractable>();
            _interactable.selectEntered.AddListener(Unanchor);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Sponge") || _isAnchored) return;
            AnchorTo(other);
        }

        private void AnchorTo(Collision other)
        {
            _interactable.enabled = false;
            _rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
            transform.SetPositionAndRotation(other.GetContact(0).point, Quaternion.FromToRotation(transform.up, -other.GetContact(0).normal));
            transform.SetParent(other.transform, true);
            _isAnchored = true;
        }

        private void Unanchor(SelectEnterEventArgs args)
        {
#pragma warning disable 0252
            // Comparing references here, need to check if this actually works
            if (args.interactableObject != _interactable) { Debug.Log("Interactable object is not equal to interactable"); return; };
#pragma warning restore
            _isAnchored = false;
            GetComponent<Collider>().enabled = true;
            _rb.isKinematic = false;
            transform.SetParent(null, true);
        }
    }

}
