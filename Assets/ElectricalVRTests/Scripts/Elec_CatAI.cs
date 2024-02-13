using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elec_CatAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Player;
    public bool FelineIncstinctON;
    public Transform LaserPointerEnd;
    Animator animator;
    float Speed;
    public Transform Paws;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("XR Origin").GetComponent<Transform>();
    }
    private void Update()
    {
        Speed = agent.velocity.magnitude;
        animator.SetFloat("Speed",Speed);
        if (!FelineIncstinctON)
        {
            agent.speed = 1.0f;
            agent.stoppingDistance = 2f;
            agent.SetDestination(Player.position);
            animator.SetBool("CatchBool", false);
        }
        else if (FelineIncstinctON)
        {
            agent.speed = 2f;
            agent.stoppingDistance = 1f;
            agent.SetDestination(LaserPointerEnd.position);
            if (Vector3.Distance(Paws.position, LaserPointerEnd.position) < 0.25)
            {
                animator.SetBool("CatchBool", true);
            }
        }       
    }
}
