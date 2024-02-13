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
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("XR Origin").GetComponent<Transform>();
    }
    private void Update()
    {
        if (!FelineIncstinctON)
        {
            agent.speed = 1.0f;
            agent.stoppingDistance = 2f;
            agent.SetDestination(Player.position);
        }
        else if (FelineIncstinctON)
        {
            agent.speed = 2f;
            agent.stoppingDistance = 0.5f;
            agent.SetDestination(LaserPointerEnd.position);
        }

    }
}
