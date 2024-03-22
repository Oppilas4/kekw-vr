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
                Juho_VihollinenHeath healt = target.GetComponent<Juho_VihollinenHeath>();
                healt.TakeDamage();
                Juho_Dissolve dissolve = target.GetComponent<Juho_Dissolve>();
                color = dissolve.color;
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
            GameObject upperHull = hull.CreateUpperHull(target);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Juho_Dissolve matTest = slicedObject.AddComponent<Juho_Dissolve>();
        matTest.color = color;
        matTest.oringinalDisolveMat = dissolveMat;
        matTest.RenewTheInfo();
        matTest.StartDissolve();
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        rb.mass = .8f;
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
    }
}
