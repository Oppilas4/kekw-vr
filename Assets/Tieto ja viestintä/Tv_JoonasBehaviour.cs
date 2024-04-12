using UnityEngine;
using UnityEngine.AI;

public class Tv_JoonasBehaviour : MonoBehaviour
{
    public Transform sitPoint; // The point where the NPC sits
    public Transform standPoint; // The point where the NPC stands up
    public Transform targetPoint; // The point where the NPC walks to when the player approaches
    public float sitTime = 5f; // How long the NPC stays seated before standing up
    public float walkSpeed = 1.5f; // Walking speed of the NPC

    private NavMeshAgent agent;
    private Animator animator;
    public bool isSeated = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Initially, NPC is seated
        Sit();
    }

    void Update()
    {
        // Check if the NPC is seated and idle
        if (isSeated && !animator.GetBool("isWalking"))
        {
            // Start monologue animation or behavior
            // For example, play idle animation or trigger monologue
        }
    }

    // Function to make the NPC sit down
    void Sit()
    {
        agent.enabled = false; // Disable NavMeshAgent while sitting
        transform.position = sitPoint.position;
        transform.rotation = sitPoint.rotation;
        isSeated = true;
        Invoke("StandUp", sitTime); // Call StandUp() after sitTime seconds
    }

    // Function to make the NPC stand up and walk to the target point
    public void StandUp()
    {
        animator.Play("JoonasStandUp");
        isSeated = false;
    }

    // Function to handle NPC reaching the stand point
    public void ReachedStandPoint()
    {
        print("moving");
        agent.enabled = true; // Enable NavMeshAgent
        agent.SetDestination(targetPoint.position); // Move to target point after reaching stand point
        //animator.SetBool("isWalking", true); // Trigger walking animation
    }

    // Function to handle NPC reaching the target point
    public void ReachedTargetPoint()
    {
        animator.SetBool("isWalking", false); // Stop walking animation
        // Perform any additional behavior or dialogue once the NPC reaches the target point
    }

    // Function to handle player entering the trigger area

}