using UnityEngine;

public class VegetableController : MonoBehaviour
{
    public VegetableData vegetableData;

    // You can use this method to retrieve the vegetable name
    public string GetVegetableName()
    {
        if (vegetableData != null)
        {
            return vegetableData.vegetableName;
        }
        else
        {
            Debug.LogError("VegetableData is not assigned to " + gameObject.name);
            return null;
        }
    }
}
