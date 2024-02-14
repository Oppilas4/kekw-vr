using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_CatAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Player;
    public Transform LaserPointerEnd;
    Animator animator;
    float Speed;
    public Transform Paws;
    public GameObject RamiPos;

    public Vector3 walkPoint;
    public bool walkPointSet,FelineIncstinctON, RamiOn,RoutineGoing;
    public float walkPointRange;
    public LayerMask whatIsGround;
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
        if (RamiOn)
        {
            Zoomies();
        }
        else if (!FelineIncstinctON)
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
    private void Zoomies()
    {
        agent.speed = 3;
        agent.stoppingDistance = 0;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);

        }
           
       
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        StartCoroutine(DontGetStuck());
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    IEnumerator DontGetStuck()
    {
        if (!RoutineGoing)
        {
            RoutineGoing = true;
            yield return new WaitForSeconds(5);
            walkPointSet = false;
            RoutineGoing = false;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ShredEye")
        {
            animator.SetTrigger("RamiOn");
            other.GetComponent<XRBaseInteractable>().enabled = false;
            other.gameObject.transform.parent = RamiPos.transform;
            other.gameObject.transform.position = RamiPos.transform.position;
            other.gameObject.transform.rotation = RamiPos.transform.rotation;
            other.enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Animator>().SetTrigger("Ridin");           
            RamiOn = true;
        }
    }
}
