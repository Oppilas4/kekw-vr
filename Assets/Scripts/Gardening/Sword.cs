using System.Collections;
using UnityEngine;
using EzySlice;

namespace Gardening
{
    public class Sword : MonoBehaviour
    {
        [SerializeField]
        private Transform _sliceStart;
        [SerializeField]
        private Transform _sliceEnd;
        [SerializeField]
        private VelocityEstimator _velocityEstimator;
        [SerializeField]
        private LayerMask _sliceableLayer;

        private bool _canSlice = true;
        private float _sliceCooldown = 0.5f;

        private void FixedUpdate()
        {
            bool hasHit = Physics.Linecast(_sliceStart.position, _sliceEnd.position, out RaycastHit hit/*, _sliceableLayer*/);

            if (hasHit && _canSlice)
            {
                GameObject target = hit.transform.gameObject;

                if (target.GetComponent<SliceablePlant>() != null)
                {
                    Slice(target);
                    StartCoroutine(StartCooldown());
                }
            }
        }

        private void Slice(GameObject target)
        {
            Debug.Log("Slice called");
            Vector3 velocity = _velocityEstimator.GetVelocityEstimate();
            Vector3 planeNormal = Vector3.Cross(_sliceEnd.position - _sliceStart.position, velocity);
            planeNormal.Normalize();

            SlicedHull hull = target.Slice(_sliceEnd.position, planeNormal);

            if (hull != null)
            {
                var sliceable = target.GetComponent<SliceablePlant>();
                Material insideMaterial = sliceable.sliceMaterial;
                GameObject upperHull = hull.CreateUpperHull(target, insideMaterial);
                SetupSlicedComponent(upperHull, sliceable);

                GameObject lowerHull = hull.CreateLowerHull(target, insideMaterial);
                SetupSlicedComponent(lowerHull, sliceable);

                Destroy(target);
            }
        }

        private void SetupSlicedComponent(GameObject slicedObject, SliceablePlant sliceable)
        {
            int interactableLayer = LayerMask.NameToLayer("Default");

            if (interactableLayer != -1)
            {
                slicedObject.layer = interactableLayer;
                Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
                rb.mass = 0.1f;
                MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
                collider.convex = true;

                // Add the vegetableController to the sliced object
                var newSlicedScript = slicedObject.AddComponent<SliceablePlant>();
                newSlicedScript.sliceMaterial = sliceable.sliceMaterial;
            }
        }

        private IEnumerator StartCooldown()
        {
            _canSlice = false;
            yield return new WaitForSeconds(_sliceCooldown);
            _canSlice = true;
        }
    }
}
