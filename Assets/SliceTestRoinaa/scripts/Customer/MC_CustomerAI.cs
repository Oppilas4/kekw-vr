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
    public float rotationSpeed = 90f;

    public Animator customerAni;

    public Transform customerHand;
    public Transform customerMouth;
    private Transform foodItem;

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

        if (navAgent.remainingDistance < 0.1)
        {
            customerAni.SetBool("Moving", false);
        }

        if(!leaving)
        {
            // Smoothly rotate the customer to match the Y rotation of the seat
            Quaternion targetRotation = Quaternion.Euler(0f, targetSeat.rotation.eulerAngles.y, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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
            customerAni.SetBool("Moving", true);

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

    public void GetFood()
    {
        customerAni.SetTrigger("Eat");
        Transform tablePosition = seatManager.GetTablePositionFromSeatLocation(targetSeat);
        foodItem = tablePosition.transform.GetChild(0);
        foodItem.transform.SetParent(customerHand);
        foodItem.transform.localPosition = Vector3.zero;
        foodItem.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }
    
    public void ChangeFoodTransform()
    {
        foodItem.transform.SetParent(customerMouth);
        foodItem.transform.localPosition = Vector3.zero;
        foodItem.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void DestroyFoodItem()
    {
        Destroy(foodItem.gameObject);
        LeaveRestaurant();
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
        customerAni.SetBool("Moving", true);
    }
}
