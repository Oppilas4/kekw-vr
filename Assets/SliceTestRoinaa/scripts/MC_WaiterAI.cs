using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MC_WaiterAI : MonoBehaviour
{
    private MC_SeatManager seatManager;

    public Transform waiterArea;
    public List<Transform> currentCustomers = new List<Transform>();
    public List<Transform> customerSeats = new List<Transform>();

    public Transform pickUpLoc;

    public Transform foodSlot;
    public GameObject foodItem;

    private Transform tablePosition;
    public float orderPromptTime = 2f; // Time to prompt the order (replace with your desired value)

    private NavMeshAgent navMeshAgent;
    private Task currentTask;
    private bool isTaskInProgress = false;
    private bool isFoodReady = false;
    private bool isFoodDelivery = false;

    private Transform deliverLocation;
    // Dictionary to hold the waiting positions for each order
    private Dictionary<int, Transform> orderWaitingPositions = new Dictionary<int, Transform>();
    private Dictionary<Transform, CustomerController> customerControllers = new Dictionary<Transform, CustomerController>();



    private void Start()
    {
        OrderManager orderManager = FindObjectOfType<OrderManager>();
        if (orderManager != null)
        {
            orderManager.OnOrderPlaced += OnOrderPlaced;
        }

        CompletedDishArea completedDishArea = FindObjectOfType<CompletedDishArea>();
        if (completedDishArea != null)
        {
            completedDishArea._completeOrder.AddListener(OnOrderComplete);
            completedDishArea._objectToDeliver.AddListener(SetFoodItem);
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        seatManager = FindAnyObjectByType<MC_SeatManager>();
        currentTask = Task.None;
        StartCoroutine(WaitForNewTask());
    }

    private void OnEnable()
    {
        MC_SeatManager.OnSeatTaken += HandleSeatTaken;
    }

    private void OnDisable()
    {
        MC_SeatManager.OnSeatTaken -= HandleSeatTaken;
    }

    private void HandleSeatTaken(Transform seatTransform, Transform waitingPosition)
    {
        currentCustomers.Add(waitingPosition);
        customerSeats.Add(seatTransform);
    }

    // Method to handle the OnOrderPlaced event
    private void OnOrderPlaced(int orderId, Transform waitingPosition)
    {
        // Add the order ID and waiting position to the dictionary
        orderWaitingPositions[orderId] = waitingPosition;

        // Find the customer controller associated with the seat
        Transform customerSeat = seatManager.GetSeatFromWaitingPosition(waitingPosition);
        if (seatManager.linkSeatAndCustomer.TryGetValue(customerSeat, out CustomerController customerController))
        {
            // Save the customer controller in the dictionary
            customerControllers[customerSeat] = customerController;
        }
        else
        {
            Debug.LogWarning("No customer controller found for seat: " + customerSeat);
        }
    }

    private void OnOrderComplete(int orderId)
    {
        if (orderWaitingPositions.TryGetValue(orderId, out Transform waitingPosition))
        {
            deliverLocation = waitingPosition;
            orderWaitingPositions.Remove(orderId);
            isFoodReady = true;
        }
        
    }
    private void SetFoodItem(GameObject FoodObject)
    {
        foodItem = FoodObject;
    }
    private void SetDishToDeliver(GameObject FoodObject)
    {
        // Set the foodObject's parent as the foodSlot so the object follows the waiter's hand
        FoodObject.transform.SetParent(foodSlot);

        // Optionally, you may want to reset the local position and rotation of the foodObject
        FoodObject.transform.localPosition = Vector3.zero;
        FoodObject.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);

        // Get the Rigidbody component of the FoodObject
        Rigidbody foodRigidbody = FoodObject.GetComponent<Rigidbody>();

        // Check if a Rigidbody component is found
        if (foodRigidbody != null)
        {
            // Set the Rigidbody to kinematic
            foodRigidbody.isKinematic = true;
        }
    }

    private void SetFoodOnTable(GameObject FoodObject, Transform waitingPosition)
    {
        // Set the foodObject's parent as the foodSlot so the object follows the waiter's hand
        FoodObject.transform.SetParent(tablePosition);

        // Optionally, you may want to reset the local position and rotation of the foodObject
        FoodObject.transform.localPosition = Vector3.zero;

        // Find the customer controller associated with the seat
        Transform customerSeat = seatManager.GetSeatFromWaitingPosition(waitingPosition);
        if (customerControllers.TryGetValue(customerSeat, out CustomerController customerController))
        {
            // Call a function in the customer controller
            customerController.OrderReady();
        }
        else
        {
            Debug.LogWarning("No customer controller found for seat: " + customerSeat);
        }
    }

    public void RemoveOrder(int orderId)
    {
        if (orderWaitingPositions.TryGetValue(orderId, out Transform waitingPosition))
        {
            orderWaitingPositions.Remove(orderId);
        }
    }

    private IEnumerator WaitForNewTask()
    {
        while (true)
        {
            if (!isTaskInProgress)
            {
                // Check for new tasks, e.g., when a new customer arrives
                yield return StartCoroutine(FindNewTask());
            }
            yield return new WaitForSeconds(3f); // Check for new customers every 3 seconds
        }
    }

    private IEnumerator FindNewTask()
    {
        if (isFoodReady)
        {
            currentTask = Task.PickUpFood;
            StartCoroutine(PerformTask());
            isFoodReady= false;
        }
        else if (isFoodDelivery)
        {
            currentTask = Task.DeliverFood;
            StartCoroutine(PerformTask());
            isFoodDelivery= false;
        }
        // Implement logic to find new tasks (e.g., find a new customer)
        else if (currentCustomers.Count > 0)
        {
            // Set the task and start the corresponding behavior
            currentTask = Task.GoToCustomerTable;
            yield return StartCoroutine(PerformTask());
        }
        else
        {
            currentTask = Task.GoToWaiterArea;
            yield return StartCoroutine(PerformTask());
        }
    }

    private IEnumerator PerformTask()
    {
        isTaskInProgress = true;

        switch (currentTask)
        {
            case Task.PickUpFood:
                navMeshAgent.SetDestination(pickUpLoc.position);

                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.1f)
                {
                    yield return null;
                }

                // After picking up the food, set the next task to DeliverFood
                isFoodDelivery = true;
                SetDishToDeliver(foodItem);
                currentTask = Task.DeliverFood;
                break;

            case Task.DeliverFood:
                // Navigate to the waiting position and deliver the order
                navMeshAgent.SetDestination(deliverLocation.position);
                Debug.Log("Delivery location");
                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.1f)
                {
                    yield return null;
                }

                // Arrived at the waiting position, get the corresponding table position
                tablePosition = seatManager.GetTablePositionFromWaitingLocation(deliverLocation);

                // Set the foodObject's parent as the table's position
                SetFoodOnTable(foodItem, deliverLocation);

                currentTask = Task.GoToWaiterArea;
                break;


            case Task.GoToCustomerTable:
                // Implement logic to navigate to the customer's table
                navMeshAgent.SetDestination(currentCustomers[0].transform.position);

                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.1f)
                {
                    yield return null;
                }

                // Arrived at the customer's table, retrieve the CustomerController
                Transform customerSeat = customerSeats[0];
                currentCustomers.RemoveAt(0);
                customerSeats.RemoveAt(0);
                CustomerController customerController = null;
                if (seatManager.linkSeatAndCustomer.TryGetValue(customerSeat, out customerController))
                {
                    // Now you have access to the CustomerController
                    // Call the PlaceOrder method
                    customerController.PlaceOrder();
                }
                else
                {
                    Debug.LogWarning("No customer controller found for seat: " + customerSeat);
                }

                currentTask = Task.TakeOrder;
                break;

            case Task.TakeOrder:

                // Simulate order prompt delay
                yield return new WaitForSeconds(orderPromptTime);

                // Order is prompted, go back to the waiter area
                currentTask = Task.GoToWaiterArea;
                break;

            case Task.GoToWaiterArea:
                // Implement logic to navigate back to the waiter area
                navMeshAgent.SetDestination(waiterArea.position);

                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.1f)
                {
                    yield return null;
                }

                // Arrived at the waiter area, prompt the order
                currentTask = Task.None;
                break;

            // Add more cases for different tasks as needed

            default:
                break;
        }

        isTaskInProgress = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (var kvp in orderWaitingPositions)
        {
            if (kvp.Value != null)
            {
                Gizmos.DrawSphere(kvp.Value.position, 0.5f);
            }
        }
    }
}

public enum Task
{
    None,
    GoToCustomerTable,
    TakeOrder,
    GoToWaiterArea,
    PickUpFood,
    DeliverFood,
    // Add more tasks as needed
}