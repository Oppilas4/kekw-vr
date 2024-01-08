using UnityEngine;

public class SteakController : MonoBehaviour
{
    public Transform topHalf;
    public Transform bottomHalf;
    public Material cookedMat;

    // Method to be called when cooking the steak
    public void CookSteak(float currentCookingTime, float totalCookingTime)
    {
        // Update cooking progress
        currentCookingTime += Time.deltaTime;

        // Check if the steak is fully cooked
        if (currentCookingTime >= totalCookingTime)
        {
            // Perform actions for fully cooked steak (e.g., change material of the appropriate half)
            ChangeMaterial();
        }
    }

    void ChangeMaterial()
    {
        Debug.Log("Changing material");

        float dotProduct = Vector3.Dot(transform.up, Vector3.up);
        float threshold = 0.99f; // Adjust the threshold as needed

        if (dotProduct > threshold)
        {
            Debug.Log("Upside down");
            if (bottomHalf.GetComponent<Renderer>() != null)
            {
                bottomHalf.GetComponent<Renderer>().material = cookedMat;
                Debug.Log("Material changed for bottom half");
            }
            else
            {
                Debug.LogError("BottomHalf does not have a Renderer component.");
            }
        }
        else if (dotProduct < -threshold)
        {
            Debug.Log("Right way up");
            if (topHalf.GetComponent<Renderer>() != null)
            {
                topHalf.GetComponent<Renderer>().material = cookedMat;
                Debug.Log("Material changed for top half");
            }
            else
            {
                Debug.LogError("TopHalf does not have a Renderer component.");
            }
        }
        else
        {
            Debug.LogWarning("Unexpected orientation. Dot Product: " + dotProduct);
        }
    }







}
