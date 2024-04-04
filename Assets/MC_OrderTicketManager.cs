using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_OrderTicketManager : MonoBehaviour
{
    private List<GameObject> ticketsInMachine = new List<GameObject>();
    private bool _occupied = false;

    public bool isOccupied(GameObject ticket)
    {
        if (ticketsInMachine.Count > 0 && ticketsInMachine[0] == ticket)
        {
            _occupied = false;
        }
        else
        {
            _occupied = true;
        }

        return _occupied;
    }

    public void removeTicketFromList(GameObject ticket)
    {
        if (ticketsInMachine.Contains(ticket))
        {
            ticketsInMachine.Remove(ticket);

            if (ticketsInMachine.Count > 0)
            {
                // Get the next ticket in the list
                GameObject nextTicket = ticketsInMachine[0];
                // Call the StartProcessing method on the next ticket
                nextTicket.GetComponent<MC_OrderTicketMovement>().Move();
            }
        }
    }

    public void addTicketToList(GameObject ticket)
    {
        ticketsInMachine.Add(ticket);
    }
}
