using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoveController : MonoBehaviour
{
    // Set the tag for the steak objects
    public string steakTag = "Steak";

    // Time it takes to cook a steak (adjust as needed)
    private float cookingTime = 0f;
    private float totalCookingTime = 3f;

    // Flag to check if currently cooking
    private bool isCooking = false;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colliding");
        // Check if the colliding object has the "Steak" tag
        if (collision.gameObject.CompareTag(steakTag) && !isCooking)
        {
            StartCoroutine(CookSteakRoutine(collision.gameObject));
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the colliding object has the "Steak" tag
        if (collision.gameObject.CompareTag(steakTag))
        {
            ResetCooking();
        }
    }

    IEnumerator CookSteakRoutine(GameObject steak)
    {
        isCooking = true;

        // Get the SteakController script from the steak object
        SteakController steakController = steak.GetComponent<SteakController>();

        // Check if the steakController is not null
        if (steakController != null)
        {
            while (cookingTime < totalCookingTime)
            {
                // Increment cooking time over time
                cookingTime += Time.deltaTime;

                // Update steak cooking progress
                steakController.CookSteak(cookingTime, totalCookingTime);

                Debug.Log("cooking");

                // Wait for the next frame
                yield return null;
            }
        }

        // Cooking is done, reset cooking time and flag
        ResetCooking();
        isCooking = false;
    }

    void ResetCooking()
    {
        // Reset cooking progress when the steak is taken away from the stove
        cookingTime = 0f;
    }
}
