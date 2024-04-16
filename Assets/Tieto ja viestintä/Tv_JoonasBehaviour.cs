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

    [SerializeField] bool isWalking;

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
        animator.SetBool("Sitting", true);
        isWalking = false; // Set isWalking to false when sitting down
    }


    public void StartToMove(Transform point)
    {
        animator.SetBool("Sitting", false);

        agent.enabled = true;
        isWalking = true;
        agent.SetDestination(point.position);
    }

    public void ReachedTheDestination()
    {
        if (agent.enabled == true)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                isWalking = false;

                if (Vector3.Distance(transform.position, sitLocation.position) < 0.5f)
                {
                    Sit();
                }

                else if (Vector3.Distance(transform.position, whereToGoStand.position) < 0.5f)
                {
                    animator.SetTrigger("Stand");
                }
            }
        }
    }
}
