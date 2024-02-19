using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_SeatManager : MonoBehaviour
{
    public Dictionary<Transform, Transform> seatToWaitingPosition = new Dictionary<Transform, Transform>();
    public List<Transform> seatPositions = new List<Transform>();
    public List<Transform> waitingPositions = new List<Transform>();
    public Transform entranceLocation;

    // Define event for when a new seat is taken
    public delegate void SeatTakenEvent(Transform seatTransform, Transform waitingPosition);
    public static event SeatTakenEvent OnSeatTaken;

    private void Start()
    {
        // Populate the seatToWaitingPosition dictionary with initial values
        for (int i = 0; i < seatPositions.Count; i++)
        {
            seatToWaitingPosition.Add(seatPositions[i], waitingPositions[i]);
        }
    }

    public Transform GetOpenSeat()
    {
        if (seatPositions.Count > 0)
        {
            // Get the first open seat
            Transform openSeat = seatPositions[0];

            // Check if the seat is null or not in the dictionary before removing it
            if (openSeat != null && seatToWaitingPosition.ContainsKey(openSeat))
            {
                // Remove the seat from the list
                seatPositions.RemoveAt(0);

                // Notify subscribers that a seat has been taken
                OnSeatTaken?.Invoke(openSeat, seatToWaitingPosition[openSeat]);

                return openSeat;
            }
            else
            {
                Debug.LogWarning("Seat not found in dictionary or is null: " + openSeat);
            }
        }
        else
        {
            Debug.LogWarning("No open seats available!");
        }
        return entranceLocation; // or some default value
    }



    public void AddSeat(Transform seatTransform)
    {
        // Check if the seat is not null before adding it back to the list
        if (seatTransform != null)
        {
            // Add the seat back to the list when the customer leaves
            seatPositions.Add(seatTransform);
        }
        else
        {
            Debug.LogWarning("Attempted to add null seat back to the list.");
        }
    }


    public Vector3 ReturnEntrance()
    {
        return entranceLocation.position;
    }
}
