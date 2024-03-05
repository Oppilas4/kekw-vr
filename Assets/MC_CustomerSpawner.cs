using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_CustomerSpawner : MonoBehaviour
{
    public OrderInteraction orderInteraction;
    public MC_SeatManager seatManager;

    private float initialSpawnDelay = 5f; // Time to wait before spawning the first customer
    private float timeBetweenCustomers = 30f; // Initial time between customers
    private float halfTimeThreshold = 150f; // Time threshold for halving the spawn time
    private void Start()
    {
        orderInteraction = FindAnyObjectByType<OrderInteraction>();
        seatManager = FindAnyObjectByType<MC_SeatManager>();
        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        // Initial delay before spawning the first customer
        yield return new WaitForSeconds(initialSpawnDelay);

        while (Time.time < halfTimeThreshold)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(timeBetweenCustomers);
        }

        // Halve the time between customers
        timeBetweenCustomers /= 2;

        while (Time.time < 300f)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(timeBetweenCustomers);
        }
    }

    private void SpawnCustomer()
    {
        if (seatManager.HasOpenSeats())
        {
            orderInteraction.CreateCustomer();
        }
    }
}
