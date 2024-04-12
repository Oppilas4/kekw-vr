using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Kekw.Common;

public class MC_TutorialBotMover : MonoBehaviour, IPause
{
    public List<Transform> navigationPositions = new List<Transform>();
    public NavMeshAgent agent;
    private float timeAtSpot;
    public float afkTime;
    

    private void Update()
    {
        if (agent.remainingDistance < 0.1f && !agent.isStopped)
        {
            timeAtSpot += Time.deltaTime;
            if (timeAtSpot >= afkTime)
            {
                timeAtSpot = 0;
                MoveToRandomPosition();
            }
        }
    }

    public void SetPause()
    {
        if (!agent.isStopped)
        {
            agent.isStopped = true;
        }
        else
        {
            UnPause();
        }
    }

    /// <summary>
    /// <seealso cref="IPause"/>
    /// </summary>
    public void UnPause()
    {
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
    }


    public void MoveToRandomPosition()
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
