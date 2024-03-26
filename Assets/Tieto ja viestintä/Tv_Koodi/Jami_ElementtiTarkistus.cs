using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jami_ElementtiTarkistus : MonoBehaviour
{
    public string elementti1, elementti2;
    // Start is called before the first frame update
    public Dictionary<string, List<string>> combinationRules = new Dictionary<string, List<string>>()
{
    { "Water", new List<string> { "Earth", "Fire", "Air" } },
    { "Earth", new List<string> { "Fire", "Air" } },
    { "Fire", new List<string> { "Air" } },
};

    // Function to combine elements
    public string CombineElements(string element1, string element2)
    {
        string combinedElement = "";

        // Check if combination rule exists for element1
        if (combinationRules.ContainsKey(element1) && combinationRules[element1].Contains(element2))
        {
            combinedElement = element1 + element2;
        }
        // Check if combination rule exists for element2 (in case the order of elements is swapped)
        else if (combinationRules.ContainsKey(element2) && combinationRules[element2].Contains(element1))
        {
            combinedElement = element2 + element1;
        }
        else
        {
            Debug.Log("Invalid combination!");
        }

        return combinedElement;
    }

    // Example function to demonstrate combining elements
    public void TestCombination(string element1, string element2)
    {
        string result = CombineElements(element1, element2);
        if (!string.IsNullOrEmpty(result))
        {
            Debug.Log("Combined: " + result);
            // Spawn the combined element or perform any other actions here
        }
    }


    public void PainaNappi()
    {
        TestCombination(elementti1, elementti2);
        Debug.Log("nappi painettu");
    }
}

