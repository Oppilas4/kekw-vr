using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_MaterialChange : MonoBehaviour
{
    // Reference to the materials you want to assign
    public Material newMaterial;

    void Start()
    {
        // Find objects with the specified names
        GameObject object1 = GameObject.Find("asdMesh.001");
        GameObject object2 = GameObject.Find("asdMesh.002");

        // Check if objects are found
        if (object1 != null && object2 != null)
        {
            // Change materials on the found objects
            ChangeObjectMaterial(object1);
            ChangeObjectMaterial(object2);
        }
        else
        {
            Debug.LogError("One or both objects not found!");
        }
    }

    void ChangeObjectMaterial(GameObject obj)
    {
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
}
