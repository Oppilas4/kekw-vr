using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_DishCalculation : MonoBehaviour
{
    public SaladBowl saladBowl;
    public PlateController plateController;
    public float thresholdSize = 0.05f;
    public float oversizedPieceDeduction = 0.1f; // Deduction per oversized piece

    void OnEnable()
    {
        // Subscribe to the _calculateDish event when the script is enabled
        FindObjectOfType<CompletedDishArea>()._calculateDish.AddListener(CheckIfCurrentDish);
    }

    void OnDisable()
    {
        // Unsubscribe from the _calculateDish event when the script is disabled to prevent memory leaks
        FindObjectOfType<CompletedDishArea>()._calculateDish.RemoveListener(CheckIfCurrentDish);
    }

    private void CheckIfCurrentDish(DishInfo dishInfo)
    {
        // Check if this plate is the current dish
        if (CompletedDishArea.currentDish == gameObject)
        {
            Debug.Log("This is the current dish: " + dishInfo.DishName);
            CalculateDish(dishInfo.DishName, dishInfo.SteakTemperature);
        }
    }

    private void CalculateDish(string dishName, string steakTemperature)
    {
        if (dishName == "Salad")
        {
            if (saladBowl != null)
            {
                saladBowl.CalculateDish();
            }
        }
        else if (dishName == "Steak")
        {
            // Find the Plate Area child
            Transform plateArea = transform.Find("PlateArea");
            if (plateArea != null)
            {
                CalculatePotatoes();
                // Loop through all children of the Plate Area
                foreach (Transform child in plateArea)
                {
                    // Check if the child has a SteakController component
                    SteakController steakController = child.GetComponent<SteakController>();
                    if (steakController != null)
                    {
                        // Call OnCalculateDish on the SteakController
                        steakController.OnCalculateDish(steakTemperature);
                        break;
                    }
                }
            }
        }
        else if (dishName == "Fries")
        {
            CalculateFries();
        }
    }

    private void CalculatePotatoes()
    {
        // Initialize score variables
        float baseScore = 50f;
        float dishScore = baseScore;
        int potatoCount = 0;
        int perfectPotatoCount = 2; // Ideal amount of potatoes for the perfect steak dish

        // Get the vegetable counts dictionary
        Dictionary<GameObject, int> vegetableCounts = plateController.GetVegetableCounts();

        // Check if there are any vegetables other than "Potato"
        foreach (var pair in vegetableCounts)
        {
            if (pair.Key.CompareTag("Potato"))
            {
                // Get the vegetable controller from each potato object
                VegetableController vegController = pair.Key.GetComponent<VegetableController>();
                if (vegController != null)
                {
                    // Check if the potato is not cooked
                    if (vegController.isCooked == false)
                    {
                        // Deduct points for not cooked potatoes
                        dishScore -= baseScore * 0.25f; // Arbitrary deduction, adjust as needed
                    }
                    else
                    {
                        // Increment the count of cooked potatoes
                        potatoCount += pair.Value;
                    }
                }
            }
            else
            {
                if (!pair.Key.CompareTag("Steak"))
                {
                    // Deduct points if there is something else on the plate other than "Potato"
                    dishScore -= baseScore * 0.5f; // Arbitrary deduction, adjust as needed
                }
                
            }
        }

        if (potatoCount != perfectPotatoCount)
        {
            // Deduct points for having less than the ideal amount of potatoes
            int missingPotatoCount = perfectPotatoCount - potatoCount;
            if (missingPotatoCount > 0)
            {
                float missingPotatoDeduction = missingPotatoCount * 10f; // Deduct 10 points per missing potato
                dishScore -= missingPotatoDeduction;
            }
        }

        // Update the game manager with the calculated score
        DishScoreManager.Instance.UpdateScore(dishScore);
    }

    private void CalculateFries()
    {
        // Get the vegetable counts dictionary
        Dictionary<GameObject, int> vegetableCounts = plateController.GetVegetableCounts();

        // Initialize score variables
        float baseScore = 100f;
        float dishScore = baseScore;
        int friesCount = 0;
        int perfectFriesCount = 20; // Ideal amount of fries

        bool anyPotatoNotCooked = false; // Flag to check if any potato is not cooked
                                         // Check if there are any vegetables other than "Potato"
        foreach (var pair in vegetableCounts)
        {
            if (pair.Key.CompareTag("Potato"))
            {
                if (anyPotatoNotCooked == false)
                {
                    // Get the vegetable controller from each potato object
                    VegetableController vegController = pair.Key.GetComponent<VegetableController>();
                    if (vegController != null)
                    {
                        // Check if the potato is not cooked
                        if (vegController.isCooked == false)
                        {
                            anyPotatoNotCooked = true;
                        }
                    }
                }

                // Calculate the volume of each potato and deduct points for oversized pieces
                float _pieceSize = CalculatePieceSize(pair.Key);
                Debug.Log(_pieceSize);
                // Count the number of fries
                friesCount += pair.Value;

                if (_pieceSize > thresholdSize)
                {
                    // Deduct points for oversized pieces
                    float oversizedDeduction = Mathf.Clamp01((_pieceSize - thresholdSize) / thresholdSize) * oversizedPieceDeduction;
                    dishScore -= (baseScore * oversizedDeduction);
                }
            }
            else
            {
                // Deduct points if there is something else on the plate other than "Potato"
                dishScore -= baseScore * 0.5f; // Arbitrary deduction, adjust as needed
            }
        }

        // Adjust base score if any potato is not cooked
        if (anyPotatoNotCooked)
        {
            dishScore *= 0.5f; // Reduce base score by 50% if any potato is not cooked
        }

        // Deduct points for having less than the ideal amount of fries
        int missingFriesCount = perfectFriesCount - friesCount;
        if (missingFriesCount > 0)
        {
            float missingFriesDeduction = missingFriesCount * 2f; // Deduct 2 points per missing fry
            dishScore -= missingFriesDeduction;
        }

        // Update the game manager with the calculated score
        DishScoreManager.Instance.UpdateScore(dishScore);
    }


    private float CalculatePieceSize(GameObject piece)
    {
        MeshFilter meshFilter = piece.GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            Transform pieceTransform = piece.transform;
            Vector3 localScale = pieceTransform.localScale;

            float totalVolume = 0f;

            for (int i = 0; i < triangles.Length; i += 3)
            {
                Vector3 v1 = Vector3.Scale(vertices[triangles[i]], localScale);
                Vector3 v2 = Vector3.Scale(vertices[triangles[i + 1]], localScale);
                Vector3 v3 = Vector3.Scale(vertices[triangles[i + 2]], localScale);

                // Calculate the volume of the triangle and add it to the total volume
                totalVolume += Vector3.Dot(Vector3.Cross(v1, v2), v3) / 6.0f;
            }

            // Return the total volume of the piece
            return Mathf.Abs(totalVolume);
        }
        else
        {
            Debug.LogWarning("No MeshFilter found on the potato GameObject.");
            return 0f;
        }
    }


}
