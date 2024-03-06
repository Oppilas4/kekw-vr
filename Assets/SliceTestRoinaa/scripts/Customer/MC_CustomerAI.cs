using UnityEngine;
using UnityEngine.AI;

public class MC_CustomerAI : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private MC_SeatManager seatManager;
    private CustomerController customerController;

    private Transform targetSeat;
    public Transform waitingPosition; // New variable to store the waiting position

    private float timeInRestaurant = 0f;
    public bool orderPlaced = false;
    private bool leaving = false;
    private float distanceThreshold = 0.1f; // Adjust this threshold as needed

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        seatManager = FindObjectOfType<MC_SeatManager>();
        customerController = GetComponent<CustomerController>();

        // Check if the required components are present
        if (navAgent == null || seatManager == null)
        {
            Debug.LogError("Missing required components!");
            Destroy(this); // Remove the script if components are missing
            return;
        }

        // Get the first open seat position and move towards it
        MoveToOpenSeat();
    }

    private void Update()
    {
        if (orderPlaced)
        {
            // Increment the time spent in the restaurant
            timeInRestaurant += Time.deltaTime;
        }

        // Check if it's time to leave (60 seconds)
        if (timeInRestaurant >= 60f && navAgent.remainingDistance < distanceThreshold && orderPlaced && !leaving)
        {
            LeaveRestaurant();
        }

        if (orderPlaced && navAgent.remainingDistance < distanceThreshold && timeInRestaurant >= 65f)
        {
            // The agent has reached its destination, destroy the GameObject
            Destroy(gameObject);
        }
    }

    void MoveToOpenSeat()
    {
        // Get the open seat and waiting position from the seat manager
        SeatInfo seatInfo = seatManager.GetOpenSeat();
        targetSeat = seatInfo.OpenSeat;
        waitingPosition = seatInfo.WaitingPosition;

        if (targetSeat.position != Vector3.zero)
        {
            // Move the customer to the open seat
            navAgent.SetDestination(targetSeat.position);

            // Link the customer to the seat
            seatManager.LinkCustomerToSeat(targetSeat, customerController);
        }
        else
        {
            // No open seats, handle accordingly (e.g., leave the restaurant)
            Debug.Log("No open seats, customer leaving.");
            Destroy(gameObject);
        }
    }

    void LeaveRestaurant()
    {
        MC_WaiterAI waiterAI = FindAnyObjectByType<MC_WaiterAI>();
        waiterAI.RemoveOrder(customerController.customer.currentOrder.orderId);
        leaving = true;
        Vector3 entranceLocation = seatManager.ReturnEntrance();
        // Ensure that the seat being returned is a valid seat
        if (seatManager.seatToWaitingPosition.ContainsKey(targetSeat))
        {
            // Remove the customer's association from the seat
            seatManager.linkSeatAndCustomer.Remove(targetSeat);

            seatManager.AddSeat(targetSeat);
        }
        else
        {
            // Handle the case where the seat is not valid
            Debug.LogWarning("Invalid seat being returned: " + targetSeat);
        }
        navAgent.SetDestination(entranceLocation);
    }
}
