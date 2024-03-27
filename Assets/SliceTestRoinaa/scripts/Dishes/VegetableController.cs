using UnityEngine;

public class VegetableController : MonoBehaviour
{
    public VegetableData vegetableData;

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
            // Update the VegetableData to reflect that the vegetable is cooked
            vegetableData.isCooked = true;

            Debug.Log(vegetableData.isCooked);

            // Perform any additional actions required after boiling
            // For example, changing the appearance or playing a sound
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
}
