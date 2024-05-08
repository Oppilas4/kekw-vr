using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_CustomerSpawner : MonoBehaviour
{
    public OrderInteraction orderInteraction;
    public MC_SeatManager seatManager;

    private float initialSpawnDelay = 5f; // Time to wait before spawning the first customer
    private float timeBetweenCustomers = 45f; // Initial time between customers
    private float halfTimeThreshold = 150f; // Time threshold for halving the spawn time

    private int totalCustomers = 0; // Total customers created
    private int customersServed = 0; // Customers served and left
    private bool gameTimeHasRunOut = false;
    private bool endGameTriggered = false;

    private float elapsedTime;
    private float targetTime;
    private float targetHalfTime;
    private void Start()
    {
        orderInteraction = FindAnyObjectByType<OrderInteraction>();
        seatManager = FindAnyObjectByType<MC_SeatManager>();
        StartCoroutine(SpawnCustomers());
        elapsedTime = Time.time;
        targetTime = elapsedTime + 300f;
        targetHalfTime = elapsedTime + halfTimeThreshold;
    }

    private void Update()
    {
        if (totalCustomers == customersServed && gameTimeHasRunOut && !endGameTriggered)
        {
            TriggerEndGameState();
        }
    }

    private IEnumerator SpawnCustomers()
    {
        // Initial delay before spawning the first customer
        yield return new WaitForSeconds(initialSpawnDelay);

        while (Time.time < targetHalfTime)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(timeBetweenCustomers);
        }

        // Halve the time between customers
        timeBetweenCustomers /= 2;

        while (Time.time < targetTime)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(timeBetweenCustomers);
        }

        if (Time.time >= targetTime && !gameTimeHasRunOut)
        {
            gameTimeHasRunOut = true;
        }
    }

    private void SpawnCustomer()
    {
        if (seatManager.HasOpenSeats())
        {
            totalCustomers++;
            orderInteraction.CreateCustomer();
        }
    }

    public void CustomerLeft()
    {
        customersServed++; // Increment customers served
    }

    void TriggerEndGameState()
    {
        endGameTriggered = true;
        // Implement your end game state logic here
        Debug.Log("All customers have left. Triggering end game state...");
        DishScoreManager.Instance.EndGamePoints();
    }
}
