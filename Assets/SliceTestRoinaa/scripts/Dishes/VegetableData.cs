using UnityEngine;

[CreateAssetMenu(fileName = "VegetableData", menuName = "ScriptableObjects/VegetableData", order = 1)]
public class VegetableData : ScriptableObject
{
    public string vegetableName;
    public Material insideMaterial;
    public AudioClip _audioClip;

    // Constructor that takes a string argument
    public VegetableData(string name)
    {
        vegetableName = name;
    }
}
