using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Custom class to hold open seat and waiting position
public class SeatInfo
{
    public Transform OpenSeat { get; set; }
    public Transform WaitingPosition { get; set; }
}

public class MC_SeatManager : MonoBehaviour
{
    public Dictionary<Transform, Transform> seatToWaitingPosition = new Dictionary<Transform, Transform>();
    public Dictionary<Transform, CustomerController> linkSeatAndCustomer = new Dictionary<Transform, CustomerController>();
    public List<Transform> seatPositions = new List<Transform>();
    public List<Transform> waitingPositions = new List<Transform>();
    public Transform entranceLocation;

    public List<Transform> tablePositions = new List<Transform>();
    public Dictionary<Transform, Transform> waitingToTablePosition = new Dictionary<Transform, Transform>();

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

        for (int i = 0; i < tablePositions.Count; i++)
        {
            waitingToTablePosition.Add(waitingPositions[i], tablePositions[i]);
        }
    }

    public Transform GetSeatFromWaitingPosition(Transform waitingPosition)
    {
        foreach (var seatAndWaitingPosition in seatToWaitingPosition)
        {
            if (seatAndWaitingPosition.Value == waitingPosition)
            {
                // Return the seat associated with the matching waiting position
                return seatAndWaitingPosition.Key;
            }
        }
        // If no matching waiting position is found, return null or handle as needed
        return null;
    }

    public Transform GetTablePositionFromWaitingLocation(Transform waitingLocation)
    {
        // Check if the waiting location exists in the dictionary
        if (waitingToTablePosition.TryGetValue(waitingLocation, out Transform tablePosition))
        {
            // Return the corresponding table position
            return tablePosition;
        }
        else
        {
            // Handle the case when the waiting location is not found in the dictionary
            Debug.LogWarning("Table position not found for waiting location: " + waitingLocation);
            return null; // or another default value, depending on your requirements
        }
    }


    public void LinkCustomerToSeat(Transform seatTransform, CustomerController customer)
    {
        if (seatTransform != null && customer != null)
        {
            linkSeatAndCustomer[seatTransform] = customer;
        }
        else
        {
            Debug.LogWarning("Attempted to link a null seat or customer.");
        }
    }

    // Modify the return type of GetOpenSeat to return SeatInfo
    public SeatInfo GetOpenSeat()
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

                // Create SeatInfo object to hold open seat and waiting position
                SeatInfo seatInfo = new SeatInfo
                {
                    OpenSeat = openSeat,
                    WaitingPosition = seatToWaitingPosition[openSeat]
                };

                // Notify subscribers that a seat has been taken
                OnSeatTaken?.Invoke(seatInfo.OpenSeat, seatInfo.WaitingPosition);

                // Return SeatInfo object
                return seatInfo;
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

        // Return a default SeatInfo object or handle this case accordingly
        return new SeatInfo { OpenSeat = entranceLocation, WaitingPosition = entranceLocation };
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

    public bool HasOpenSeats()
    {
        return seatPositions.Count > 0;
    }
}
