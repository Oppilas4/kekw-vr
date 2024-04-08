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
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);

        if (hasHit && canSlice)
        {
            GameObject target = hit.transform.gameObject;

            // Check if the target has the VegetableController script before slicing
            if (target.GetComponent<Vegetable_Handler>() != null)
            {
                Slice(target);
                StartCoroutine(StartCooldown());
            }
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            Vegetable_Handler vegetableController = GetVegetableController(target);
            Material insideMaterial = vegetableController.vegetableData.insideMaterial;
            GameObject upperHull = hull.CreateUpperHull(target, insideMaterial);
            SetupSlicedComponent(upperHull, vegetableController);

            GameObject lowerHull = hull.CreateLowerHull(target, insideMaterial);
            SetupSlicedComponent(lowerHull, vegetableController);

            Destroy(target);
        }
    }

    private Vegetable_Handler GetVegetableController(GameObject vegetable)
    {
        Vegetable_Handler vegetableController = vegetable.GetComponent<Vegetable_Handler>();

        if (vegetableController != null)
        {
            return vegetableController;
        }
        else
        {
            Debug.LogError("VegetableController is not found on " + vegetable.name);
            return null;
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject, Vegetable_Handler vegetableController)
    {
        int interactableLayer = LayerMask.NameToLayer("Interactable");

        if (interactableLayer != -1)
        {
            slicedObject.layer = interactableLayer;
            Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
            rb.mass = 0.1f;
            MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
            collider.convex = true;

            // Add the vegetableController to the sliced object
            Vegetable_Handler slicedVegetableController = slicedObject.AddComponent<Vegetable_Handler>();

            slicedVegetableController.vegetableData = vegetableController.vegetableData;

            slicedObject.tag = slicedVegetableController.vegetableData.vegetableName;
        }
    }


    IEnumerator StartCooldown()
    {
        canSlice = false; // Set the flag to prevent slicing
        yield return new WaitForSeconds(sliceCooldown); // Wait for the cooldown duration
        canSlice = true; // Allow slicing again after cooldown
    }
}
