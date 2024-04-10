using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR.Interaction.Toolkit;

public class TV_SwordSlice : MonoBehaviour
{
    [SerializeField] bool isEcoloop = false;
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public Material dissolveMat;
    public LayerMask layer;
    Color color;

    public float knockback = 30;

    [System.Obsolete]
    private void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, layer);
        if (hasHit)
        {
            SendHaptics();
            GameObject target = hit.transform.gameObject;
            if (target.CompareTag("Jami_Enemy"))
            {
                if(!isEcoloop)
                {
                    TV_EnemyHealth healt = target.GetComponent<TV_EnemyHealth>();
                    healt.TakeDamage();
                }
                TV_DisolveScript dissolve = target.GetComponent<TV_DisolveScript>();
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

        if (hull != null)
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
        TV_DisolveScript matTest = slicedObject.AddComponent<TV_DisolveScript>();
        matTest.color = color;
        matTest.oringinalDisolveMat = dissolveMat;
        matTest.RenewTheInfo();
        matTest.StartDissolve();
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        rb.mass = .8f;
        rb.constraints = RigidbodyConstraints.None;
        Vector3 direction = (slicedObject.transform.position - startSlicePoint.position).normalized;
        rb.AddForce(direction * knockback);
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
    }


        [System.Obsolete]
        public void SendHaptics()
        {
            XRBaseControllerInteractor hand = (XRBaseControllerInteractor)GetComponent<XRBaseInteractable>().selectingInteractor;
            
        }
}
