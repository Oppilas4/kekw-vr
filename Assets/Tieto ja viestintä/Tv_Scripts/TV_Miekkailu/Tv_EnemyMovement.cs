using Oculus.Interaction.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tv_EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public bool runAway;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    private bool isCalculatingWalkPoint = false;
    //Patrolling
    private bool isFirstTime = true;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Main Camera").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
            
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if(runAway)
        {
            Patroling();
        }


        if(!runAway)
        {

        
            // When player is in sight range stars chaseing player
            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();        
            }
            // When in attack range moves to arena
            if (playerInSightRange && playerInAttackRange)
            {
                agent.speed = 0f;
            }
        }


    }

    // Runs towards player
    private void ChasePlayer()
    {
        agent.speed = 6;

        agent.SetDestination(player.position);
    }


    private void Patroling()
    {
        agent.speed = 3;
        if (!walkPointSet && !isCalculatingWalkPoint)
        {
            if (isFirstTime)
            {
                SearchWalkPoint();
                isFirstTime = false;
            }
            else
            {
                StartCoroutine(WaitAndSearchWalkPoint());
            }
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private IEnumerator WaitAndSearchWalkPoint()
    {
        isCalculatingWalkPoint = true;
        yield return new WaitForSeconds(1f); // Adjust the time to wait here (3 seconds in this case)
        SearchWalkPoint();
        isCalculatingWalkPoint = false;
    }

    private void SearchWalkPoint()
    {
        // Generate random points within the walk point range
        Vector3 randomDirection = Random.insideUnitSphere * walkPointRange;
        randomDirection += transform.position;
        NavMeshHit navHit;

        // Check if the random point is on the NavMesh
        if (NavMesh.SamplePosition(randomDirection, out navHit, walkPointRange, NavMesh.AllAreas))
        {
            // If the point is on the NavMesh, set it as the walk point
            walkPointSet = true;
            walkPoint = navHit.position;
        }
    }
}
