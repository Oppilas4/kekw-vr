using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Juho_Slice : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public Material dissolveMat;
    public LayerMask layer;
    Color color;

    private void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, layer);
        if(hasHit)
        {
            GameObject target = hit.transform.gameObject;
            if(target.CompareTag("Jami_Enemy"))
            {
                Juho_VihollinenHeath heath = target.GetComponent<Juho_VihollinenHeath>();
                color = heath.color;
                Slice(target);
            }
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, dissolveMat);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, dissolveMat);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Juho_MaterialTest matTest = slicedObject.AddComponent<Juho_MaterialTest>();
        matTest.color = color;
        matTest.material = dissolveMat;
        matTest.StartDissolve();
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        rb.mass = .8f;
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
    }
}
