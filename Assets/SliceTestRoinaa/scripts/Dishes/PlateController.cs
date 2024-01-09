using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;

public class PlateController : MonoBehaviour
{
    // This list will store the sliced vegetable pieces on the plate
    private List<GameObject> vegetablePiecesOnPlate = new List<GameObject>();

    public float delayBeforeParenting = 0.5f; // Adjust the delay time as needed
    
    private IEnumerator OnTriggerEnter(Collider other)
    {
        VegetableController vegetableController = other.GetComponent<VegetableController>();
        // Check if the entering object is on the interactable layer
        if (vegetableController != null)
        {
            // Wait for the specified delay before parenting
            yield return new WaitForSeconds(delayBeforeParenting);

            // Remove XRGrabInteractable script if it exists
            XRGrabInteractable grabInteractable = other.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                Destroy(grabInteractable);
            }

            Destroy(GetComponentInChildren<Rigidbody>());

            // Parent the sliced vegetable to the plate
            other.transform.parent = transform;

            // Add the sliced vegetable to the list
            vegetablePiecesOnPlate.Add(other.gameObject);
        }
    }

    // Example function to clear all vegetables from the plate
    public void ClearPlate()
    {
        foreach (var vegetable in vegetablePiecesOnPlate)
        {
            Destroy(vegetable);
        }

        // Clear the list
        vegetablePiecesOnPlate.Clear();
    }

    // Helper function to check if a specific ingredient is on the plate
    private bool ContainsIngredient(string ingredientName)
    {
        foreach (var vegetable in vegetablePiecesOnPlate)
        {
            VegetableController vegetableController = vegetable.GetComponent<VegetableController>();
            if (vegetableController != null && vegetableController.GetVegetableName() == ingredientName)
            {
                return true;
            }
        }
        return false;
    }
    
}
