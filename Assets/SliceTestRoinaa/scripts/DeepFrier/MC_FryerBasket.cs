using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MC_FryerBasket : MonoBehaviour
{
    // This list will store the sliced vegetable pieces on the plate
    private List<GameObject> vegetablePiecesOnPlate = new List<GameObject>();
    private List<Rigidbody> vegetableRigidbodies = new List<Rigidbody>();

    public float delayBeforeParenting = 0.5f; // Adjust the delay time as needed
    public float requiredRotationAngle = 100f; // Rotation angle in degrees for unparenting

    private bool isRotated = false;

    private void Update()
    {
        // Check if the basket has been rotated by the required angle
        if (!isRotated && IsBasketTilted())
        {
            isRotated = true;
            unParentContent();
        }
    }


    private IEnumerator OnTriggerEnter(Collider other)
    {
        VegetableController vegetableController = other.GetComponent<VegetableController>();
        // Check if the entering object is on the interactable layer
        if (vegetableController != null)
        {
            // Wait for the specified delay before parenting
            yield return new WaitForSeconds(delayBeforeParenting);
            if (other.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                vegetableRigidbodies.Add(rb);
                // Parent the sliced vegetable to the basket
                other.transform.parent = transform;

                // Add the sliced vegetable to the list
                vegetablePiecesOnPlate.Add(other.gameObject);
            }
        }
    }

    private void unParentContent()
    {
        if (vegetablePiecesOnPlate.Count > 0)
        {
            foreach (GameObject vegetablePiece in vegetablePiecesOnPlate)
            {
                vegetablePiece.transform.parent = null;
            }

            foreach (Rigidbody rb in vegetableRigidbodies)
            {
                rb.isKinematic = false;
            }

            vegetablePiecesOnPlate.Clear();
            vegetableRigidbodies.Clear();
        }

        isRotated = false;
    }

    private bool IsBasketTilted()
    {
        // Check if the basket is tilted forward or to the side based on the x and z-axis rotations
        float xRotation = transform.rotation.eulerAngles.x;
        float zRotation = transform.rotation.eulerAngles.z;

        // Normalize the rotation angles to be between -180 and 180 degrees
        xRotation = (xRotation > 180f) ? xRotation - 360f : xRotation;
        zRotation = (zRotation > 180f) ? zRotation - 360f : zRotation;

        // Check if the pot is tilted beyond the pour threshold in either x or z direction
        return (Mathf.Abs(xRotation) > requiredRotationAngle || Mathf.Abs(zRotation) > requiredRotationAngle);
    }
}
