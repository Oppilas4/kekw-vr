using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MC_WaiterAI : MonoBehaviour
{
    public Transform waiterArea;
    public List<Transform> currentCustomers = new List<Transform>();

    public float orderPromptTime = 2f; // Time to prompt the order (replace with your desired value)

    private NavMeshAgent navMeshAgent;
    private Task currentTask;
    private bool isTaskInProgress = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        // Implement logic to find new tasks (e.g., find a new customer)
        if (currentCustomers.Count > 0)
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
            case Task.GoToCustomerTable:
                // Implement logic to navigate to the customer's table
                navMeshAgent.SetDestination(currentCustomers[0].transform.position);

                while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.1f)
                {
                    yield return null;
                }

                // Arrived at the customer's table, take the order
                currentCustomers.RemoveAt(0);
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
}

public enum Task
{
    None,
    GoToCustomerTable,
    TakeOrder,
    GoToWaiterArea,
    // Add more tasks as needed
}