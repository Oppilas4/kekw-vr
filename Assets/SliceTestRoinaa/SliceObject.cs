using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;

    private bool canSlice = true;
    private float sliceCooldown = 0.5f; // Cooldown duration

    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer); //Create a line between the start and end points, if object in the sliceableLayer is hit, initiate slice
        if (hasHit && canSlice) // Check if the cooldown is over
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
            StartCoroutine(StartCooldown()); // Start the cooldown
        }
    }

    public void Slice(GameObject target)
    {
        //Create a vector for the direction of the slice
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            //Create upper and lower parts of the sliced object
            GameObject upperHull = hull.CreateUpperHull(target);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        int interactableLayer = LayerMask.NameToLayer("Interactable");

        if (interactableLayer != -1)
        {
            // Set the layer of the sliced object to "Interactable"
            slicedObject.layer = interactableLayer;

            // Add a Rigidbody and a convex MeshCollider as before
            Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
            rb.mass = 0.1f;
            MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
            collider.convex = true;
            slicedObject.tag = "VegetablePiece";
        }
    }

    IEnumerator StartCooldown()
    {
        canSlice = false; // Set the flag to prevent slicing
        yield return new WaitForSeconds(sliceCooldown); // Wait for the cooldown duration
        canSlice = true; // Allow slicing again after cooldown
    }
}
