using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashCanController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the "Dish" tag
        if (other.CompareTag("Dish"))
        {
            PlateController plateController = other.GetComponentInChildren<PlateController>();
            // Clear the plate using the PlateController script
            if (plateController != null )
            {
                plateController.ClearPlate();
                Debug.Log("Plate cleared!"); // Optional debug log
            }
        }
    }
}
