using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_SakariVesi : MonoBehaviour
{
    public Material fillMaterial; // Assign your material in the Unity Editor
    public float fillSpeed = 0.5f; // Speed at which the material fills
    public float decreaseSpeed = 2f; // Speed at which the material decreases

    private bool isFilling = false;
    public float currentFill = 0f;

    void Update()
    {
        if (isFilling)
        {
            Debug.Log("Hsaodasä");
            // If filling is true, increase fill
            currentFill += fillSpeed * Time.deltaTime;
            currentFill = Mathf.Clamp01(currentFill); // Clamp between 0 and 1
        }
        else
        {
            Debug.Log("Hyvä");
            // If filling is false, decrease fill
            currentFill -= decreaseSpeed * Time.deltaTime;
            currentFill = Mathf.Clamp01(currentFill); // Clamp between 0 and 1
        }

        // Apply the current fill amount to the material
        fillMaterial.SetFloat("_Fill", currentFill);
    }

    // Function to set the filling status
    public void SetFilling(bool fill)
    {
        isFilling = fill;
    }
}
