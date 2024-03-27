using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jami_ElementtiTarkistus : MonoBehaviour
{
    public string elementti1, elementti2;
    public GameObject waterPrefab, earthPrefab, firePrefab, airPrefab;

    // Start is called before the first frame update
    public Dictionary<string, string> combinationRules = new Dictionary<string, string>()
    {
        { "WaterEarth", "Mud" },
        { "WaterFire", "Steam" },
        { "WaterAir", "Rain" },
        { "EarthFire", "Lava" },
        { "EarthAir", "Dust" },
        { "FireAir", "Smoke" }
    };

    // Function to combine elements
    public string CombineElements(string element1, string element2)
    {
        string combinedElement = "";

        // Check if combination rule exists for element1 and element2
        if (combinationRules.ContainsKey(element1 + element2))
        {
            combinedElement = combinationRules[element1 + element2];
        }
        else if (combinationRules.ContainsKey(element2 + element1))
        {
            combinedElement = combinationRules[element2 + element1];
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
            SpawnElement(result);
        }
        else
        {
            // If no combination is found, spawn single element
            SpawnElement(element1);
            SpawnElement(element2);
        }
    }

    // Spawn element based on combined element or single element
    void SpawnElement(string element)
    {
        switch (element)
        {
            case "Water":
                Debug.Log(element);
                Instantiate(waterPrefab, transform.position, Quaternion.identity);
                break;
            case "Earth":
                Debug.Log(element);
                Instantiate(earthPrefab, transform.position, Quaternion.identity);
                break;
            case "Fire":
                Debug.Log(element);
                Instantiate(firePrefab, transform.position, Quaternion.identity);
                break;
            case "Air":
                Debug.Log(element);
                Instantiate(airPrefab, transform.position, Quaternion.identity);
                break;
            case "Mud":
                Debug.Log(element);
                // Spawn Mud prefab
                break;
            case "Steam":
                Debug.Log(element);
                // Spawn Steam prefab
                break;
            case "Rain":
                Debug.Log(element);
                // Spawn Rain prefab
                break;
            case "Lava":
                Debug.Log(element);
                // Spawn Lava prefab
                break;
            case "Dust":
                Debug.Log(element);
                // Spawn Dust prefab
                break;
            case "Smoke":
                Debug.Log(element);
                // Spawn Smoke prefab
                break;
            default:
                Debug.LogWarning("No prefab defined for " + element);
                break;
        }
    }

    public void PainaNappi()
    {
        TestCombination(elementti1, elementti2);
        Debug.Log("nappi painettu");
    }
}