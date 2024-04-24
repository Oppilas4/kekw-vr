using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_ElementChecker : MonoBehaviour
{
    public string elementti1, elementti2;
    public GameObject waterPrefab, earthPrefab, firePrefab, airPrefab, dustPrefab, rainPrefab, lavaPrefab, steamPrefab, smokePrefab, mudPrefab;
    public Transform elementSpawnTransform;
    public GameObject activeElement;

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
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                //waterPrefab.SetActive(true);
                activeElement = waterPrefab;
                activeElement.SetActive(true);
                break;
            case "Earth":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                // Instantiate(earthPrefab, transform.position, Quaternion.identity);
                activeElement = earthPrefab;
                activeElement.SetActive(true);
                break;
            case "Fire":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                //firePrefab.SetActive(true);
                activeElement = firePrefab;
                activeElement.SetActive(true);
                break;
            case "Air":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                //airPrefab.SetActive(true);
                activeElement = airPrefab;
                activeElement.SetActive(true);
                break;
            case "Mud":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                // Spawn Mud prefab
                //mudPrefab.SetActive(true);
                activeElement = mudPrefab;
                activeElement.SetActive(true);
                break;
            case "Steam":
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                Debug.Log(element);
                // Spawn Steam prefab
                //steamPrefab.SetActive(true);
                activeElement = steamPrefab;
                activeElement.SetActive(true);
                break;
            case "Rain":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                // Spawn Rain prefab
                //rainPrefab.SetActive(true);
                activeElement = rainPrefab;
                activeElement.SetActive(true);
                break;
            case "Lava":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                //lavaPrefab.SetActive(true);
                activeElement = rainPrefab;
                activeElement.SetActive(true);
                break;
            case "Dust":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                //dustPrefab.SetActive(true);
                // Spawn Dust prefab
                activeElement = dustPrefab;
                activeElement.SetActive(true);
                break;
            case "Smoke":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                //smokePrefab.SetActive(true);
                // Spawn Smoke prefab
                activeElement = smokePrefab;
                activeElement.SetActive(true);
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