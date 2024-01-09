using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static OrderManager;

public class CompletedDishArea : MonoBehaviour
{
    public UnityEvent _calculateDish = new UnityEvent();
    public UnityEvent<string> _sendSteakTemperature = new UnityEvent<string>();

    public static GameObject currentDish; // Store the current dish in the serving area
    public string ticketDishName; // Store the current order
    private string steakTemperature;

    private void OnEnable()
    {
        // Subscribe to the _orderReady event when the script is enabled
        FindObjectOfType<OrderBell>()._orderReady.AddListener(OnOrderReady);
    }

    private void OnDisable()
    {
        // Unsubscribe from the _orderReady event when the script is disabled to prevent memory leaks
        FindObjectOfType<OrderBell>()._orderReady.RemoveListener(OnOrderReady);
    }

    public void SetCurrentDish(GameObject dish)
    {
        currentDish = dish;
    }

    public void ClearCurrentDish()
    {
        currentDish = null;
        ticketDishName = null;
        steakTemperature = null;
    }

    private void OnOrderReady()
    {
        // Method to be executed when the _orderReady event happens
        CheckForDishAndTicket();
    }

    public void CheckForDishAndTicket()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Vector3 colliderSize = boxCollider.size;

        Collider[] colliders = Physics.OverlapBox(transform.position, colliderSize, transform.rotation);

        GameObject foundDish = null;
        int ticketCount = 0;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Dish"))
            {
                foundDish = collider.gameObject;
            }
            else if (collider.CompareTag("OrderTicket"))
            {
                ticketCount++;
                OrderTicket orderTicket = collider.GetComponent<OrderTicket>();
                if (orderTicket != null)
                {
                    ticketDishName = orderTicket.dishNameText.text;
                    steakTemperature = orderTicket.steakTemperatureText.text;
                }
            }
        }

        if (ticketCount == 1 && foundDish != null)
        {
            Debug.Log("Dish and Order Ticket detected in the serving area");

            SetCurrentDish(foundDish); // Set the current dish
            _calculateDish.Invoke(); // Trigger the _calculateDish event
            if (ticketDishName == "Dish: Steak")
            {
                _sendSteakTemperature.Invoke(steakTemperature);
            }

            ClearCurrentDish(); // Clear the current dish after calculations
        }
        else if (ticketCount > 1)
        {
            Debug.Log("Error: Multiple Order Tickets in the serving area");
            // Display your error message here
        }
        else
        {
            Debug.Log("Either Dish or Order Ticket is missing in the serving area");
            // Display your error message here
        }
    }
}
