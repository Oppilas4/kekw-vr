using UnityEngine;

public class OrderInteraction : MonoBehaviour
{
    public OrderManager orderManager;
    public GameObject customerPrefab; // Assign your customer prefab in the Inspector

    private int customerIdCounter = 1;

    void Update()
    {
        // Check for spacebar input to create a customer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateCustomer();
        }
    }

    public void CreateCustomer()
    {
        GameObject customerObject = Instantiate(customerPrefab);
        Customer customer = new Customer(customerIdCounter++);
        customerObject.GetComponent<CustomerController>().Initialize(customer, orderManager);

        Debug.Log($"Customer {customer.customerId} created.");
    }

    // Call this function when a customer places an order
    public void PlaceOrder(Customer customer)
    {
        orderManager.GenerateRandomOrder(customer);
    }

    // Call this function when the chef completes an order
    public void CompleteOrder(int orderId)
    {
        orderManager.CompleteOrder(orderId);
    }
}
