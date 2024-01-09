using UnityEngine;
using System.Collections;
using System;

public class SteakController : MonoBehaviour
{
    public Transform topHalf;
    public Transform bottomHalf;

    public Material uncookedMat;
    public Material rawMat;
    public Material mediumMat;
    public Material wellDoneMat;
    public Material burnedMat;

    private string steakOrientation;

    private CookingStage currentCookingStage;
    private float totalCookingTime = 10f;
    private bool isCooking = false;

    private SideCookTime bottomHalfCookTime = new SideCookTime();
    private SideCookTime topHalfCookTime = new SideCookTime();

    private Coroutine cookingCoroutine;


    void OnEnable()
    {
        // Subscribe to the _calculateDish event when the script is enabled
        FindObjectOfType<CompletedDishArea>()._sendSteakTemperature.AddListener(OnCalculateDish);
    }

    void OnDisable()
    {
        // Unsubscribe from the _calculateDish event when the script is disabled to prevent memory leaks
        FindObjectOfType<CompletedDishArea>()._sendSteakTemperature.RemoveListener(OnCalculateDish);
    }

    public enum CookingStage
    {
        Uncooked,
        Raw,
        Medium,
        WellDone,
        Burned
    }

    public void StartCooking()
    {
        if (!isCooking)
        {
            // Determine the orientation of the steak
            float dotProduct = Vector3.Dot(transform.up, Vector3.up);
            float threshold = 0.99f; // Adjust the threshold as needed

            if (dotProduct > threshold)
            {
                Debug.Log("Steak is right-side-up");
                steakOrientation = "upRight";
                isCooking = true;
                cookingCoroutine = StartCoroutine(CookSteakRoutine(bottomHalfCookTime));
            }
            else if (dotProduct < -threshold)
            {
                Debug.Log("Steak is upside-down");
                steakOrientation = "upsideDown";
                isCooking = true;
                cookingCoroutine = StartCoroutine(CookSteakRoutine(topHalfCookTime));
            }
            else
            {
                Debug.LogWarning("Unexpected orientation. Dot Product: " + dotProduct);
            }
        }
    }

    public void StopCooking()
    {
        if (isCooking)
        {
            isCooking = false;
            if (cookingCoroutine != null)
            {
                StopCoroutine(cookingCoroutine);
            }
        }
    }

    IEnumerator CookSteakRoutine(SideCookTime sideCookTime)
    {
        while (isCooking)
        {
            sideCookTime.Value += Time.deltaTime;

            // Determine the cooking stage based on the current cooking time
            DetermineCookingStage(sideCookTime.Value);

            // Set the CurrentStage property
            sideCookTime.CurrentStage = currentCookingStage;

            ChangeMaterial();

            yield return null;
        }
    }

    void DetermineCookingStage(float sideCookTime)
    {
        float progress = sideCookTime / totalCookingTime;
        if (progress < 0.3f)
        {
            currentCookingStage = CookingStage.Uncooked;
        }
        else if (progress < 0.5f)
        {
            currentCookingStage = CookingStage.Raw;
        }
        else if (progress < 0.7f)
        {
            currentCookingStage = CookingStage.Medium;
        }
        else if (progress < 0.9f)
        {
            currentCookingStage = CookingStage.WellDone;
        }
        else
        {
            currentCookingStage = CookingStage.Burned;
        }
    }

    void ChangeMaterial()
    {
        Debug.Log("Changing material");

        switch (currentCookingStage)
        {
            case CookingStage.Uncooked:
                SetMaterial(uncookedMat);
                break;

            case CookingStage.Raw:
                SetMaterial(rawMat);
                break;

            case CookingStage.Medium:
                SetMaterial(mediumMat);
                break;

            case CookingStage.WellDone:
                SetMaterial(wellDoneMat);
                break;

            case CookingStage.Burned:
                SetMaterial(burnedMat);
                break;

            default:
                Debug.LogWarning("Unexpected cooking stage: " + currentCookingStage);
                break;
        }
    }

    void SetMaterial(Material newMaterial)
    {

        if (steakOrientation == "upsideDown")
        {
            if (topHalf.GetComponent<Renderer>() != null)
            {
                topHalf.GetComponent<Renderer>().material = newMaterial;
            }
            else
            {
                Debug.LogError("TopHalf does not have a Renderer component.");
            }
        }
        else if (steakOrientation == "upRight")
        {
            if (bottomHalf.GetComponent<Renderer>() != null)
            {
                bottomHalf.GetComponent<Renderer>().material = newMaterial;
            }
            else
            {
                Debug.LogError("BottomHalf does not have a Renderer component.");
            }
        }
    }

    public class SideCookTime
    {
        public float Value { get; set; }
        public SteakController.CookingStage CurrentStage { get; set; }
    }


    public void OnCalculateDish(string temperature)
    {
        try
        {
            // Split the string on the colon and take the second part, trim whitespace
            string tempStatus = temperature.Split(':')[1].Trim();

            Debug.Log("Temperature status received: " + tempStatus);

            // Convert the trimmed temperature status to the corresponding CookingStage
            CookingStage desiredStage;
            if (string.Equals(tempStatus, "Raw", StringComparison.OrdinalIgnoreCase))
            {
                desiredStage = CookingStage.Raw;
            }
            else if (string.Equals(tempStatus, "Medium", StringComparison.OrdinalIgnoreCase))
            {
                desiredStage = CookingStage.Medium;
            }
            else if (string.Equals(tempStatus, "WellDone", StringComparison.OrdinalIgnoreCase))
            {
                desiredStage = CookingStage.WellDone;
            }
            else
            {
                throw new ArgumentException($"Invalid temperature status: {tempStatus}");
            }

            Debug.Log("Desired Cooking Stage: " + desiredStage);
            // Calculate the score based on the desired stage and the actual stage of each side of the steak
            int score = CalculateScore(desiredStage, topHalfCookTime.CurrentStage, bottomHalfCookTime.CurrentStage);

            // Display the score
            Debug.Log($"Score: {score}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in OnCalculateDish: {ex}");
        }
    }




    public int CalculateScore(CookingStage desiredStage, CookingStage topHalfStage, CookingStage bottomHalfStage)
    {
        Debug.Log("calculating");
        int score = 100;

        // Calculate the score for the top half of the steak
        int topHalfScore = Mathf.Abs((int)desiredStage - (int)topHalfStage);
        score -= topHalfScore * 10;

        // Calculate the score for the bottom half of the steak
        int bottomHalfScore = Mathf.Abs((int)desiredStage - (int)bottomHalfStage);
        score -= bottomHalfScore * 10;

        return score;
    }


}
