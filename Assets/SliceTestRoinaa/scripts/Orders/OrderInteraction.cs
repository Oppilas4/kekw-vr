using UnityEngine;

public class OrderInteraction : MonoBehaviour
{
    public OrderManager orderManager;
    public GameObject customerPrefab; // Assign your customer prefab in the Inspector
    public Transform spawnLocation;

    private int customerIdCounter = 1;

    void Update()
    {
        // Check for spacebar input to create a customer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateCustomer();
        }
    }

    void OnEnable()
    {
        // Subscribe to the _calculateDish event when the script is enabled
        FindObjectOfType<CompletedDishArea>()._completeOrder.AddListener(CompleteOrder);
    }

    void OnDisable()
    {
        // Unsubscribe from the _calculateDish event when the script is disabled to prevent memory leaks
        FindObjectOfType<CompletedDishArea>()._completeOrder.RemoveListener(CompleteOrder);
    }

    public void CreateCustomer()
    {
        GameObject customerObject = Instantiate(customerPrefab, spawnLocation.position, Quaternion.identity);
        Customer customer = new Customer(customerIdCounter++);
        customerObject.GetComponent<CustomerController>().Initialize(customer, orderManager);

        Debug.Log($"Customer {customer.customerId} created.");
    }

    // Call this function when the chef completes an order
    public void CompleteOrder(int orderId)
    {
        orderManager.CompleteOrder(orderId);
    }
}
