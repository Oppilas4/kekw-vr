using UnityEngine;

[CreateAssetMenu(fileName = "VegetableData", menuName = "ScriptableObjects/VegetableData", order = 1)]
public class VegetableData : ScriptableObject
{
    public string vegetableName;
    public Material insideMaterial;

    // Constructor that takes a string argument
    public VegetableData(string name)
    {
        vegetableName = name;
    }
}
