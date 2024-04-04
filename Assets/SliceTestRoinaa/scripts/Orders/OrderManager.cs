using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class OrderManager : MonoBehaviour
{
    public List<string> availableDishes;
    public UnityEvent orderPlacedEvent;
    public GameObject orderTicketPrefab;
    public Transform ticketSpawnLoc;
    private MC_OrderTicketManager orderTicketManager;

    private List<Order> activeOrders = new List<Order>();
    

    // Define a delegate for the event
    public delegate void OrderPlacedDelegate(int orderId, Transform waitingPosition);
    // Define the event
    public event OrderPlacedDelegate OnOrderPlaced;

    public enum SteakTemperature
    {
        Raw,
        Medium,
        WellDone
    }

    private void Start()
    {
        orderTicketManager = FindAnyObjectByType<MC_OrderTicketManager>();
    }

    public void GenerateRandomOrder(Customer customer, Transform waitingPosition)
    {
        Order newOrder = new Order
        {
            orderId = Random.Range(1000, 9999),
            dishName = availableDishes[Random.Range(0, availableDishes.Count)],
            expirationTime = Time.time + 60f // 60 seconds expiration time (adjust as needed)
        };

        // Assign a random temperature to the steak if the dish is a steak
        if (newOrder.dishName == "Steak")
        {
            newOrder.steakTemperature = (SteakTemperature)Random.Range(0, 3); // 0-2 corresponds to Raw, Medium, WellDone
        }

        // Assign the order to the customer
        customer.currentOrder = newOrder;

        activeOrders.Add(newOrder);
        orderPlacedEvent.Invoke();

        // Instantiate an order ticket
        InstantiateOrderTicket(newOrder);

        // Invoke the event after the order is placed
        OnOrderPlaced?.Invoke(newOrder.orderId, waitingPosition);
    }

    private void InstantiateOrderTicket(Order order)
    {
        // Instantiate the order ticket prefab
        GameObject orderTicketObject = Instantiate(orderTicketPrefab, ticketSpawnLoc.position, Quaternion.identity);

        order.orderTicketObject = orderTicketObject;

        // Get the OrderTicket component
        OrderTicket orderTicket = orderTicketObject.GetComponent<OrderTicket>();

        // Pass the steak temperature to the order ticket
        orderTicket.SetOrderInfo(order.orderId, order.dishName, order.steakTemperature);

        if(orderTicketManager != null)
        {
            orderTicketManager.addTicketToList(orderTicketObject);
        }
    }

    private void CheckOrderStatus()
    {
        for (int i = activeOrders.Count - 1; i >= 0; i--)
        {
            if (Time.time > activeOrders[i].expirationTime)
            {
                // Order expired
                // Trigger an event or handle expiration as needed
                Debug.Log("order " + activeOrders[i].orderId + " expired");

                // Destroy the order ticket GameObject
                if (activeOrders[i].orderTicketObject != null)
                {
                    Destroy(activeOrders[i].orderTicketObject);
                }

                activeOrders.RemoveAt(i);
            }
        }
    }

    public void CompleteOrder(int orderId)
    {
        // Find the order by ID and handle completion
        Order completedOrder = activeOrders.Find(order => order.orderId == orderId);
        if (completedOrder != null)
        {
            // Handle order completion (e.g., score points, remove from active orders)
            activeOrders.Remove(completedOrder);
        }
    }

    private void Update()
    {
        CheckOrderStatus();
    }
}
