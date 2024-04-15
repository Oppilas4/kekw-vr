using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;
using static UnityEngine.GraphicsBuffer;

public class Tv_Sakari : MonoBehaviour
{
    public float throwForce;
    public float upForce;
   public Transform startPos;
    public Transform player;
    public float speed;
    public bool sakariAnger;
    public float sakariRange;
    private NavMeshAgent navMeshAgent;
    public Transform attachTransformHand;
    public GameObject bottle;
    private bool carryingBottle = false;
    public Transform door;
    public bool isBottle = false;
    public Transform throwTransform;
    public float rotationSpeed = 0.5f;
    private SakariState currentState = SakariState.Idle;
    // Start is called before the first frame update
    public enum SakariState
    {
        Idle,
        Throwing,
        Returning
    }
    void Start()
    {
        player = GameObject.Find("XR Origin").GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case SakariState.Idle:
                UpdateIdleState();
                break;
            case SakariState.Throwing:
                UpdateThrowingState();
                break;
            case SakariState.Returning:
                UpdateReturningState();
                break;
        }
    }
    void UpdateReturningState()
    {
        // Move towards the start position
        navMeshAgent.SetDestination(startPos.position);

        // Debugging
        Debug.Log("Remaining distance: " + navMeshAgent.remainingDistance);

        // Check if the NPC has reached the start position
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f))
        {
            // Transition back to Idle state
            currentState = SakariState.Idle;
            Debug.Log("Returned to start position.");
        }
    }

    void UpdateIdleState()
    {
        if (!carryingBottle && !sakariAnger)
        {
            if (isBottle)
            {
                navMeshAgent.SetDestination(bottle.transform.position);
            }
        }
        else if (!sakariAnger)
        {
            navMeshAgent.SetDestination(door.position);

            // Check if the NPC has reached the destination (door)
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f))
            {
                // Transition to Throwing state
                currentState = SakariState.Throwing;
                Invoke("ThrowBottleOut", 1.0f);
            }

            // Calculate the direction from the NPC to the door
            Vector3 lookDirection = (door.position - transform.position).normalized;

            // Set the y component of the direction to zero to ensure the NPC looks horizontally
            lookDirection.y = 0f;

            // Make the NPC look away from the room
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    void UpdateThrowingState()
    {
        // No need to do anything here, as the behavior is handled in ThrowBottleOut
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState == SakariState.Idle && other.CompareTag("tv_pullo") && !sakariAnger && !carryingBottle)
        {
            PickUpBottle();
        }
    }

    void PickUpBottle()
    {
        print("PickUp");
        Rigidbody rb = bottle.GetComponent<Rigidbody>();
        carryingBottle = true;
        // Disable the bottle GameObject
        rb.isKinematic = true;
        // Attach the bottle to the NPC's hand
        bottle.transform.parent = attachTransformHand;
        bottle.transform.localPosition = Vector3.zero; // Center the bottle in the hand
        bottle.transform.localRotation = Quaternion.identity; // Reset rotation
    }

    void SakariReturn()
    {
        navMeshAgent.speed = 3.5f;
        navMeshAgent.SetDestination(startPos.position);
        print("returning");

        // Set the rotation to match the original rotation
        transform.rotation = startPos.rotation;
    }
    void ThrowBottleOut()
    {
        print("throwbottleout");
        sakariAnger = true;
        carryingBottle = false;
        Rigidbody rb = bottle.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        bottle.transform.parent = null;

        // Calculate the direction from the throw transform to the door
        Vector3 throwDirection = (door.position - throwTransform.position).normalized;

        // Define a throw force and upward force

        // Calculate the total force vector
        Vector3 totalForce = throwDirection * throwForce + Vector3.up * upForce;

        // Apply force to the bottle gradually over time
        rb.AddForce(totalForce, ForceMode.Impulse);

        // Add torque for rotation
        float torqueStrength = 0.5f; // Adjusted torque strength
        Vector3 torqueDirection = Random.onUnitSphere; // Randomize rotation direction
        rb.AddTorque(torqueDirection * torqueStrength, ForceMode.Impulse);

        // Transition to Returning state after throwing the bottle
        currentState = SakariState.Returning;
        Invoke("SakariReturn", 1.0f);
    }
}
