using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class DishInfo
{
    public string DishName;
    public string SteakTemperature;

    public DishInfo(string dishName, string steakTemperature)
    {
        DishName = dishName;
        SteakTemperature = steakTemperature;
    }
}

public class CompletedDishArea : MonoBehaviour
{
    public UnityEvent<DishInfo> _calculateDish = new UnityEvent<DishInfo>();
    public UnityEvent<int> _completeOrder = new UnityEvent<int>();
    public UnityEvent<GameObject> _objectToDeliver = new UnityEvent<GameObject>();

    public static GameObject currentDish; // Store the current dish in the serving area
    private string steakTemperature;
    private int orderID;

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

        Vector3 colliderSize = boxCollider.size /2;

        Collider[] colliders = Physics.OverlapBox(transform.position, colliderSize, transform.rotation);

        GameObject foundDish = null;
        int ticketCount = 0;
        string ticketDishName = null;

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
                    ticketDishName = orderTicket.dishNameText.text.TrimStart("Dish: ".ToCharArray());
                    steakTemperature = orderTicket.steakTemperatureText.text;
                    if (steakTemperature != "")
                    {
                        steakTemperature = steakTemperature.Split(':')[1].Trim();
                    }
                    string orderIDText = orderTicket.orderNumberText.text;
                    string[] splitText = orderIDText.Split('#');
                    if (splitText.Length == 2)
                    {
                        if (int.TryParse(splitText[1], out int orderNumber))
                        {
                            orderID = orderNumber;
                        }
                        else
                        {
                            Debug.LogError("Error parsing order number");
                        }
                    }
                    else
                    {
                        Debug.LogError("Unexpected orderID format");
                    }

                    Destroy(collider.gameObject);
                }
            }
        }

        if (ticketCount == 1 && foundDish != null)
        {
            Debug.Log("Dish and Order Ticket detected in the serving area");

            SetCurrentDish(foundDish); // Set the current dish
            _calculateDish.Invoke(new DishInfo(ticketDishName, steakTemperature)); // Trigger the _calculateDish event
            _completeOrder.Invoke(orderID);
            _objectToDeliver.Invoke(currentDish);

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
