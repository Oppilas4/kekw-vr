using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AC_DogFindFood : MonoBehaviour
{
    public Animator dogAnimator;             // Reference to the dog's Animator
    public string eatAnimationTrigger = "Eat"; // The trigger to start the eat animation
    public float detectionRadius = 10f;      // The radius in which the dog can detect food
    public LayerMask foodLayer;              // To detect only food objects
    public float moveSpeed = 1.5f;             // Speed at which the dog moves towards food

    private NavMeshAgent navAgent;           // Reference to the dog's NavMeshAgent for movement
    private bool isEating = false;           // To check if the dog is already eating
    private Transform targetFood = null;     // The food the dog is going towards

    // Start is called before the first frame update
    void Start()
    {
        if (dogAnimator == null)
        {
            dogAnimator = GetComponent<Animator>(); // Assign the Animator if not set in the inspector
        }

        navAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component on the dog
        navAgent.speed = moveSpeed;              // Set movement speed for the NavMeshAgent
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEating)
        {
            SearchForFood(); // Keep searching for food
        }
        else
        {
            MoveTowardsFood(); // If we found food, move towards it
        }
    }

    // Search for food within the detection radius
    void SearchForFood()
    {
        // Use a sphere cast (or you could use a simple OverlapSphere) to find food in range
        Collider[] foodColliders = Physics.OverlapSphere(transform.position, detectionRadius, foodLayer);

        if (foodColliders.Length > 0) // If food is found
        {
            // Choose the closest food (you could add a loop for multiple foods)
            targetFood = foodColliders[0].transform;
            StartMovingToFood();
        }
    }

    // Start moving the dog to the food
    void StartMovingToFood()
    {
        // Start the movement towards the food
        if (targetFood != null)
        {
            // Get the dog's current Y position
            float dogYPosition = transform.position.y;

            // Set the target position, but keep the dog's current Y position
            Vector3 targetPosition = new Vector3(targetFood.position.x, dogYPosition, targetFood.position.z);

            // Set the adjusted target position as the NavMeshAgent's destination
            navAgent.SetDestination(targetPosition);
        }
    }

    // Move the dog towards the food
    void MoveTowardsFood()
    {
        // Check if the dog is close enough to the food (within a certain threshold)
        if (Vector3.Distance(transform.position, targetFood.position) <= navAgent.stoppingDistance)
        {
            // Stop the movement
            navAgent.isStopped = true;

            // Trigger the eating animation
            StartEating(targetFood);
        }
    }

    // Start the eating animation and logic
    void StartEating(Transform food)
    {
        if (!isEating)
        {
            isEating = true;
            Debug.Log("Dog found food and started eating!");

            // Trigger the "Eat" animation
            dogAnimator.SetTrigger(eatAnimationTrigger);

            // Optionally, destroy the food or hide it after eating
            Destroy(food.gameObject); // Or use food.SetActive(false); to hide the food instead

            // Stop further movement or reset any necessary variables after eating
        }
    }
}
