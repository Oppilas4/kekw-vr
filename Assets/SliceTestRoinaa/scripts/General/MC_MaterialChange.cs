using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_MaterialChange : MonoBehaviour
{
    // Reference to the materials you want to assign
    public Material newMaterial;
    public GameObject hand1;
    public GameObject hand2;

    // Store references to the original materials
    private Material originalMaterial1;
    private Material originalMaterial2;

    void Start()
    {
        // Find objects with the specified names
        GameObject object1 = GameObject.Find("asdMesh.001");
        GameObject object2 = GameObject.Find("asdMesh.002");

        hand1 = object1;
        hand2 = object2;

        // Check if objects are found
        if (object1 != null && object2 != null)
        {
            // Store references to the original materials
            StoreOriginalMaterials(object1);
            StoreOriginalMaterials(object2);

            // Change materials on the found objects
            ChangeObjectMaterial(object1, newMaterial);
            ChangeObjectMaterial(object2, newMaterial);
        }
        else
        {
            Debug.LogError("One or both objects not found!");
        }
    }

    void StoreOriginalMaterials(GameObject obj)
    {
        // Check if the object has a renderer component
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Store the original material of the object
            if (obj.name == "asdMesh.001")
            {
                originalMaterial1 = renderer.material;
            }
            else if (obj.name == "asdMesh.002")
            {
                originalMaterial2 = renderer.material;
            }
        }
        else
        {
            Debug.LogError("Object " + obj.name + " does not have a Renderer component!");
        }
    }

    void ChangeObjectMaterial(GameObject obj, Material newMaterial)
    {
        Debug.Log("Vaihto");
        // Check if the object has a renderer component
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Assign the new material to the object's renderer
            renderer.material = newMaterial;
        }
        else
        {
            Debug.LogError("Object " + obj.name + " does not have a Renderer component!");
        }
    }

    public void ChangeToOriginalMaterial()
    {
        if (hand1 != null && hand2 != null && originalMaterial1 != null && originalMaterial2 != null)
        {
            ChangeObjectMaterial(hand1, originalMaterial1);
            ChangeObjectMaterial(hand2, originalMaterial2);
        }
    }
}
