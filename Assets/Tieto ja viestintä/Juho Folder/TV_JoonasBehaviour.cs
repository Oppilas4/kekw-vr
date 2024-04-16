using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TV_JoonasBehaviour : MonoBehaviour
{
    public Transform sitLocation;
    public Transform whereToGoStand;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Sit();
    }

    private void Update()
    {
        ReachedTheDestination();
    }

    void Sit()
    {
        transform.position = sitLocation.position;
        transform.rotation = sitLocation.rotation;
        agent.enabled = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsSitting", true);
    }


    public void StartToMove(Transform point)
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsSitting", false);
        animator.SetBool("IsIdle", false);

        agent.enabled = true;
        agent.SetDestination(point.position);
    }

    public void ReachedTheDestination()
    {
        if (agent.enabled == true)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (Vector3.Distance(transform.position, sitLocation.position) < 0.5f)
                {
                    Sit();
                }

                else if (Vector3.Distance(transform.position, whereToGoStand.position) < 0.5f)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsSitting", false);
                    animator.SetBool("IsIdle", true);
                }
            }
        }
    }
}
