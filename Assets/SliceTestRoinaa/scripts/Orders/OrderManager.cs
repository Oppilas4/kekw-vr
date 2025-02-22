using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class OrderManager : MonoBehaviour
{
    public List<string> availableDishes;
    public UnityEvent orderPlacedEvent;
    public GameObject orderTicketPrefab;

    private List<Order> activeOrders = new List<Order>();

    public void GenerateRandomOrder(Customer customer)
    {
        Order newOrder = new Order
        {
            orderId = Random.Range(1000, 9999),
            dishName = availableDishes[Random.Range(0, availableDishes.Count)],
            expirationTime = Time.time + 5f // 60 seconds expiration time (adjust as needed)
        };

        // Assign the order to the customer
        customer.currentOrder = newOrder;

        activeOrders.Add(newOrder);
        orderPlacedEvent.Invoke();

        // Instantiate an order ticket
        InstantiateOrderTicket(newOrder);
    }

    private void InstantiateOrderTicket(Order order)
    {
        // Instantiate the order ticket prefab
        GameObject orderTicketObject = Instantiate(orderTicketPrefab);

        // Get the OrderTicket component
        OrderTicket orderTicket = orderTicketObject.GetComponent<OrderTicket>();

        // Set order information on the ticket
        orderTicket.SetOrderInfo(order.orderId, order.dishName);
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
