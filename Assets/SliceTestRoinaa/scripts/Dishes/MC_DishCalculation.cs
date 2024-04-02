using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_DishCalculation : MonoBehaviour
{
    public SaladBowl saladBowl;

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
        Debug.Log("Eventti aktivoitu");
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
                Debug.Log("Salaatti lasku");
                saladBowl.CalculateDish();
            }
        }
        else if (dishName == "Steak")
        {
            Debug.Log("Pihvin metsästys");
            // Find the Plate Area child
            Transform plateArea = transform.Find("PlateArea");
            if (plateArea != null)
            {
                // Loop through all children of the Plate Area
                foreach (Transform child in plateArea)
                {
                    // Check if the child has a SteakController component
                    SteakController steakController = child.GetComponent<SteakController>();
                    if (steakController != null)
                    {
                        Debug.Log("Pihvin lasku");
                        // Call OnCalculateDish on the SteakController
                        steakController.OnCalculateDish(steakTemperature);
                        break;
                    }
                }
            }
        }
    }
}
