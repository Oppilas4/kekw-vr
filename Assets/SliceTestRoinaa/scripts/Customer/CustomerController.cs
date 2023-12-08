using UnityEngine;

public class CustomerController : MonoBehaviour
{
    private Customer customer;
    private OrderManager orderManager;

    void Update()
    {
        // Check for Enter key input to place an order
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlaceOrder();
        }
    }

    public void Initialize(Customer newCustomer, OrderManager manager)
    {
        customer = newCustomer;
        orderManager = manager;
    }

    // Call this function when the customer places an order
    public void PlaceOrder()
    {
        orderManager.GenerateRandomOrder(customer);
        Debug.Log($"Customer {customer.customerId} placed an order: {customer.currentOrder.dishName} - Expiration Time: {customer.currentOrder.expirationTime}");
    }
}
