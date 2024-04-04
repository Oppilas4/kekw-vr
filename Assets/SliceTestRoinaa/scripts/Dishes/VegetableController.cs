using System.Collections.Generic;
using UnityEngine;

public class VegetableController : MonoBehaviour
{
    public VegetableData vegetableData;
    public bool isCooked = false;

    private void OnEnable()
    {
        MC_BoilingController.OnVegetableBoiled += HandleBoiledEvent;
    }

    private void OnDisable()
    {
        MC_BoilingController.OnVegetableBoiled -= HandleBoiledEvent;
    }

    public void HandleBoiledEvent(VegetableController boiledController)
    {
        // Check if this VegetableController is the one that got boiled
        if (this == boiledController)
        {
            isCooked = true;
        }
    }

    // You can use this method to retrieve the vegetable name
    public VegetableData GetVegetableData()
    {
        if (vegetableData != null)
        {
            return vegetableData;
        }
        else
        {
            Debug.LogError("VegetableData is not assigned to " + gameObject.name);
            return null;
        }
    }

    public List<Material> GetMaterials()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            return new List<Material>(renderer.materials);
        }
        else
        {
            Debug.LogError("No Renderer component found on " + gameObject.name);
            return new List<Material>();
        }
    }
}
