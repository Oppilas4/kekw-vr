using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Tv_Sakari : MonoBehaviour
{
    public Transform player;
    public float speed;
    public bool sakariAnger;
    public float sakariRange;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Origin").GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sakariAnger)
        {
            navMeshAgent.speed = 3.5f;
            navMeshAgent.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.transform.position) <= sakariRange)
            {
                navMeshAgent.speed = 0;
                sakariAnger = false;
            }
        }
    }
}
