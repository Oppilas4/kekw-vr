using UnityEngine;
using UnityEngine.AI;

public class MC_CustomerAI : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private MC_SeatManager seatManager;

    private Transform targetSeat;

    private float timeInRestaurant = 0f;
    private bool isLeaving = false;
    private float distanceThreshold = 0.1f; // Adjust this threshold as needed

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        seatManager = FindObjectOfType<MC_SeatManager>();

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
        // Increment the time spent in the restaurant
        timeInRestaurant += Time.deltaTime;

        // Check if it's time to leave (5 seconds)
        if (timeInRestaurant >= 15f && navAgent.remainingDistance < distanceThreshold && !isLeaving)
        {
            LeaveRestaurant();
            timeInRestaurant = 0;
        }

        if (isLeaving && navAgent.remainingDistance < distanceThreshold && timeInRestaurant >= 5f)
        {
            // The agent has reached its destination, destroy the GameObject
            Destroy(gameObject);
        }
    }

    void MoveToOpenSeat()
    {
        targetSeat = seatManager.GetOpenSeat();

        if (targetSeat.position != Vector3.zero)
        {
            // Move the customer to the open seat
            navAgent.SetDestination(targetSeat.position);
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
        isLeaving = true;
        Vector3 entranceLocation = seatManager.ReturnEntrance();
        // Ensure that the seat being returned is a valid seat
        if (seatManager.seatToWaitingPosition.ContainsKey(targetSeat))
        {
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
