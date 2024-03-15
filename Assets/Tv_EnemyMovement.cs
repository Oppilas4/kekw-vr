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
    

    //Patrolling
 

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
            gameObject.transform.LookAt(player);
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

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

    // Runs towards player
    private void ChasePlayer()
    {
        agent.speed = 6;

        agent.SetDestination(player.position);
    }
}
