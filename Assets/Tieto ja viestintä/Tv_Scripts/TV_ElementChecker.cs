using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_ElementChecker : MonoBehaviour
{
    public string elementti1, elementti2;
    public GameObject waterPrefab, earthPrefab, firePrefab, airPrefab, dustPrefab, rainPrefab, lavaPrefab, steamPrefab, smokePrefab, mudPrefab;
    public Transform elementSpawnTransform;
    public GameObject activeElement;

    [SerializeField] GameObject sakariPatsas;

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

    List<string> madeCombinations = new List<string>();

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

    public void TestCombination(string element1, string element2)
    {
        string result = CombineElements(element1, element2);
        if (!string.IsNullOrEmpty(result))
        {
            Debug.Log("Combined: " + result);
            SpawnElement(result);
            // Add the combination to the list of made combinations
            madeCombinations.Add(result);
            // Check if all specified combinations are made
            CheckForSakariPatsasActivation();
        }
        else
        {
            SpawnElement(element1);
            SpawnElement(element2);
        }
    }

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
                activeElement = waterPrefab;
                activeElement.SetActive(true);

                break;
            case "Earth":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                activeElement = earthPrefab;
                activeElement.SetActive(true);
                break;
            case "Fire":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                activeElement = firePrefab;
                activeElement.SetActive(true);
                break;
            case "Air":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                activeElement = airPrefab;
                activeElement.SetActive(true);
                break;
            case "Mud":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                activeElement = mudPrefab;
                activeElement.SetActive(true);
                break;
            case "Steam":
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                Debug.Log(element);
                activeElement = steamPrefab;
                activeElement.SetActive(true);
                break;
            case "Rain":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                activeElement = rainPrefab;
                activeElement.SetActive(true);
                break;
            case "Lava":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                activeElement = lavaPrefab;
                activeElement.SetActive(true);
                break;
            case "Dust":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                activeElement = dustPrefab;
                activeElement.SetActive(true);
                break;
            case "Smoke":
                Debug.Log(element);
                if (activeElement != null)
                {
                    activeElement.SetActive(false);
                }
                activeElement = smokePrefab;
                activeElement.SetActive(true);
                break;
            default:
                Debug.LogWarning("No prefab defined for " + element);
                break;
        }
    }

    void CheckForSakariPatsasActivation()
    {
        // Define the specified combinations
        List<string> specifiedCombinations = new List<string>() { "Mud", "Steam", "Rain", "Lava", "Dust", "Smoke" };

        // Check if all specified combinations are present in madeCombinations
        bool allCombinationsMade = true;
        foreach (string combination in specifiedCombinations)
        {
            if (!madeCombinations.Contains(combination))
            {
                allCombinationsMade = false;
                break;
            }
        }

        // Activate sakariPatsas if all combinations are made
        if (allCombinationsMade)
        {
            sakariPatsas.SetActive(true);
        }
    }

    public void PainaNappi()
    {
        TestCombination(elementti1, elementti2);
        Debug.Log("nappi painettu");
    }
}