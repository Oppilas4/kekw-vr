using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MC_TutorialBotMover : MonoBehaviour
{
    public List<Transform> navigationPositions = new List<Transform>();
    private NavMeshAgent agent;

    void Start()
    {
        // Get the NavMeshAgent component attached to the same GameObject
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(MoveToRandomPositionCoroutine());
    }

    IEnumerator MoveToRandomPositionCoroutine()
    {
        while (true)
        {
            MoveToRandomPosition();
            yield return new WaitForSeconds(10); // Wait for 10 seconds
        }
    }


    void MoveToRandomPosition()
    {
        // Check if there are any positions in the list
        if (navigationPositions.Count > 0)
        {
            // Select a random position from the list
            int randomIndex = Random.Range(0, navigationPositions.Count);
            Transform randomPosition = navigationPositions[randomIndex];

            // Set the destination for the agent
            agent.SetDestination(randomPosition.position);
        }
    }
}
