using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;

public class PlateController : MonoBehaviour
{
    public ParticleSystem _bubbles;
    public ParticleSystem _foam;
    public GameObject DecalProjector;
    // This list will store the sliced vegetable pieces on the plate
    private List<GameObject> vegetablePiecesOnPlate = new List<GameObject>();

    public float delayBeforeParenting = 0.5f; // Adjust the delay time as needed
    
    private IEnumerator OnTriggerEnter(Collider other)
    {
        Vegetable_Handler vegetableController = other.GetComponent<Vegetable_Handler>();
        // Check if the entering object is on the interactable layer
        if (vegetableController != null)
        {
            // Wait for the specified delay before parenting
            yield return new WaitForSeconds(delayBeforeParenting);
            if (other.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
            {
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sponge"))
        {
            if (!_foam.isPlaying)
            {
                _foam.Play();
                _bubbles.Play();
            }
            DecalProjector.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sponge"))
        {
            _foam.Stop();
            _bubbles.Stop();
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
            Vegetable_Handler vegetableController = vegetable.GetComponent<Vegetable_Handler>();
            if (vegetableController != null && vegetableController.GetVegetableData().vegetableName == ingredientName)
            {
                return true;
            }
        }
        return false;
    }

    // Helper function to return the count of each type of vegetable on the plate
    public Dictionary<GameObject, int> GetVegetableCounts()
    {
        Dictionary<GameObject, int> counts = new Dictionary<GameObject, int>();

        foreach (var vegetable in vegetablePiecesOnPlate)
        {
            if (vegetable != null)
            {
                if (counts.ContainsKey(vegetable))
                {
                    counts[vegetable]++;
                }
                else
                {
                    counts.Add(vegetable, 1);
                }
            }
        }

        return counts;
    }


}
