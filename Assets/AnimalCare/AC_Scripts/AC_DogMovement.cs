using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class AC_DogMovement : MonoBehaviour
{
    public Animator dogAnimator;             // Reference to the dog's Animator
    public string eatAnimationTrigger = "Eat"; // The trigger to start the eat animation
    public float detectionRadius = 10f;      // The radius in which the dog can detect food
    public LayerMask foodLayer;              // To detect only food objects
    public float moveSpeed = 1.5f;             // Speed at which the dog moves towards food
    public AudioSource eatingsound;

    public AC_ChecklistManager checklistManager;
    public AC_ShakingCondition shakingCondition;
    public AC_DogFoodPouring foodPouring;

    private NavMeshAgent navAgent;           // Reference to the dog's NavMeshAgent for movement
    private bool isEating = false;           // To check if the dog is already eating
    private Transform targetFood = null;     // The food the dog is going towards
    private Transform targetBall = null;     // The ball the dog is going towards
    public AC_Ball ballCheck;

    public GameObject water;
    public GameObject towel;
    public bool movetoNearTub = false;
    public bool movetoTrim = false;
    public Transform bathtub;
    public Transform trimmingTable;
    public Transform outoftub;
    public Transform start;

    public AC_DoorToggle door;
    public Transform InTub;
    bool waitingToEnterSink = false;
    public bool movedInTub = false;

    bool takeBall = false;
    //bool giveBall = false;
    public Transform mouthTransform; // Assign this in the Inspector
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
    void OnEnable()
    {
        if (navAgent == null)
            navAgent = GetComponent<NavMeshAgent>();
        if (start != null)
        {
            navAgent.Warp(start.position); // Teleport the dog to the start position
            transform.rotation = start.rotation; // Optional: match the rotation
            dogAnimator.SetFloat("Speed", 0); // Reset animation to idle
            isEating = false; // Reset eating state
            navAgent.isStopped = false;
            targetFood = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isEating)
        {
            SearchForFood(); // Keep searching for food
        }
        // Check if dog is waiting and door is now open
        if (waitingToEnterSink && door.checkOpening)
        {
            waitingToEnterSink = false;
            MoveIntoSink();
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

            if (shakingCondition.isShaking)
            {
                return;
            }
            StartMovingToFood();
        }
        else if (ballCheck.isOnGround && !takeBall)
        {
            targetBall = ballCheck.transform;
            Debug.Log("Start moving to the ball");
            StartMovingToBall();
        }
        else MoveToBathTub();
    }

    // Start moving the dog to the food
    void StartMovingToFood()
    {
        // Start the movement towards the food
        if (targetFood != null)
        {
            // Set the target position, but keep the dog's current Y position
            Vector3 targetPosition = new Vector3(targetFood.position.x, 0, targetFood.position.z);
            // Trigger the "Walk" animation
            dogAnimator.SetFloat("Speed",moveSpeed);
            Debug.Log("Walking to food");
            // Set the adjusted target position as the NavMeshAgent's destination
            navAgent.SetDestination(targetPosition);
            StopAtFood();
        }
    }
    void StopAtFood()
    {
        // Check if the dog is close enough to the food (within a certain threshold)
        if (Vector3.Distance(transform.position, targetFood.position) <= navAgent.stoppingDistance)
        {
            // Stop the movement
            navAgent.isStopped = true;
            Debug.Log("Moved");
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
            dogAnimator.SetFloat("Speed", 0);
            // Trigger the "Eat" animation
            dogAnimator.SetTrigger(eatAnimationTrigger);
            eatingsound.Play();
            StartCoroutine(WaitAndDestroy(food.gameObject)); // Or use food.SetActive(false); to hide the food instead

            checklistManager.CompleteTask(2);
            // Stop further movement or reset any necessary variables after eating
        }
    }
    private IEnumerator WaitAndDestroy(GameObject DestroyedObject)
    {
        yield return new WaitForSeconds(4f);
        foodPouring.amount = 0;
        Destroy(DestroyedObject); // Or use food.SetActive(false); to hide the food instead
        isEating = false;
        navAgent.isStopped = false;
    }
    void MoveToBathTub()
    {
        if (movetoNearTub)
        {
            // Set the target position, but keep the dog's current Y position
            Vector3 target2Position = new Vector3(bathtub.position.x, 0, bathtub.position.z);

            // Set the adjusted target position as the NavMeshAgent's destination
            navAgent.SetDestination(target2Position);

            // Start checking the destination arrival status once the dog is moving
            StartCoroutine(CheckIfDogReachedDestinationOfTub());
        }
        else if(movetoTrim)
        {
            // Set the target position, but keep the dog's current Y position
            Vector3 target4Position = new Vector3(trimmingTable.position.x, trimmingTable.position.y, trimmingTable.position.z);

            // Set the adjusted target position as the NavMeshAgent's destination
            navAgent.SetDestination(target4Position);

            // Start checking the destination arrival status once the dog is moving
            StartCoroutine(CheckIfDogReachedDestinationOfTrim());
        }
    }

    IEnumerator CheckIfDogReachedDestinationOfTub()
    {
        // Wait for the agent to start moving (it may take a brief moment for the agent to calculate the path)
        yield return new WaitUntil(() => !navAgent.pathPending);

        // Start checking the remaining distance
        while (navAgent.remainingDistance > navAgent.stoppingDistance)
        {

            // Trigger the "Walk" animation
            dogAnimator.SetFloat("Speed", moveSpeed);

            // Wait a frame before checking again, allowing other systems to run
            yield return null;
        }

        // Once the dog has reached the target
        movetoNearTub = false;
        dogAnimator.SetFloat("Speed", 0);  // Stop walking animation
        Debug.Log("Dog is near Bathtub");

        if (door.checkOpening)
        {
            StartCoroutine(Wait(1.5f));
            MoveIntoSink();
        }
        else
        {
            waitingToEnterSink = true; // Wait for door to open
        }
    }
    IEnumerator CheckIfDogReachedDestinationOfTrim()
    {
        // Wait for the agent to start moving (it may take a brief moment for the agent to calculate the path)
        yield return new WaitUntil(() => !navAgent.pathPending);

        // Start checking the remaining distance
        while (navAgent.remainingDistance > navAgent.stoppingDistance)
        {

            // Trigger the "Walk" animation
            dogAnimator.SetFloat("Speed", moveSpeed);

            // Wait a frame before checking again, allowing other systems to run
            yield return null;
        }
        // Once the dog has reached the target
        movetoTrim = false;
        dogAnimator.SetFloat("Speed", 0);  // Stop walking animation
        Debug.Log("Dog is at Trimmer");
    }
    public void AfterShower()
    {
        StartCoroutine(WaitAndMoveOut(towel));
    }
    private IEnumerator WaitAndMoveOut(GameObject towel)
    {
        yield return new WaitForSeconds(8f);
        movedInTub = false;
        Vector3 target3Position = new Vector3(outoftub.position.x, outoftub.position.y, outoftub.position.z);
        // Set the adjusted target position as the NavMeshAgent's destination
        navAgent.SetDestination(target3Position);

        // Start checking the destination arrival status once the dog is moving
        StartCoroutine(CheckIfDogReachedDestinationOut());
    }
    IEnumerator CheckIfDogReachedDestinationOut()
    {
        // Wait for the agent to start moving (it may take a brief moment for the agent to calculate the path)
        yield return new WaitUntil(() => !navAgent.pathPending);

        // Start checking the remaining distance
        while (navAgent.remainingDistance > navAgent.stoppingDistance)
        {

            // Trigger the "Walk" animation
            dogAnimator.SetFloat("Speed", moveSpeed);

            // Wait a frame before checking again, allowing other systems to run
            yield return null;
        }
        // Once the dog has reached the target
        dogAnimator.SetFloat("Speed", 0);  // Stop walking animation
        Debug.Log("Dog is Out");

        yield return new WaitForSeconds(1f);
        towel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        towel.SetActive(false);
    }
    void MoveIntoSink()
    {
        // Set the target position, but keep the dog's current Y position
        Vector3 target5Position = new Vector3(InTub.position.x, InTub.position.y, InTub.position.z);

        // Set the adjusted target position as the NavMeshAgent's destination
        navAgent.SetDestination(target5Position);

        // Start checking the destination arrival status once the dog is moving
        StartCoroutine(CheckIfDogInTub());
    }
    IEnumerator CheckIfDogInTub()
    {
        // Wait for the agent to start moving (it may take a brief moment for the agent to calculate the path)
        yield return new WaitUntil(() => !navAgent.pathPending);

        // Start checking the remaining distance
        while (navAgent.remainingDistance > navAgent.stoppingDistance)
        {
            // Trigger the "Walk" animation
            dogAnimator.SetFloat("Speed", moveSpeed);

            // Rotate the dog slightly (you can adjust the angle of rotation as needed)
            float rotationSpeed = 40f; // Adjust rotation speed
            Vector3 directionToTarget = navAgent.steeringTarget - transform.position;
            directionToTarget.y = 0; // Make sure to rotate only around the Y-axis
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget) * Quaternion.Euler(0, 15f, 0);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Wait a frame before checking again, allowing other systems to run
            yield return null;
        }
        dogAnimator.SetFloat("Speed", 0);  // Stop walking animation
        movedInTub = true;
        Debug.Log("Dog is on Bathtub");
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    public void MoveToDoor()
    {
        // Set the target position, but keep the dog's current Y position
        Vector3 target6Position = new Vector3(start.position.x, 0, start.position.z);

        // Set the adjusted target position as the NavMeshAgent's destination
        navAgent.SetDestination(target6Position);

        // Start checking the destination arrival status once the dog is moving
        StartCoroutine(CheckIfDogAtDoor());
    }
    IEnumerator CheckIfDogAtDoor()
    {
        // Wait for the agent to start moving (it may take a brief moment for the agent to calculate the path)
        yield return new WaitUntil(() => !navAgent.pathPending);

        // Start checking the remaining distance
        while (navAgent.remainingDistance > navAgent.stoppingDistance)
        {
            // Trigger the "Walk" animation
            dogAnimator.SetFloat("Speed", moveSpeed);
            // Wait a frame before checking again, allowing other systems to run
            yield return null;
        }
        dogAnimator.SetFloat("Speed", 0);  // Stop walking animation
        gameObject.SetActive(false);
    }
    
    // Start moving the dog to the food
    void StartMovingToBall()
    {
        // Start the movement towards the food
        if (targetBall != null)
        {
            // Set the target position, but keep the dog's current Y position
            Vector3 target7Position = new Vector3(targetBall.position.x, 0, targetBall.position.z);
            // Trigger the "Walk" animation
            dogAnimator.SetFloat("TakingBallSpeed", moveSpeed);
            Debug.Log("Start to move to the ball");
            // Set the adjusted target position as the NavMeshAgent's destination
            navAgent.SetDestination(target7Position);
            StopAtBall();
        }
    }
    void StopAtBall()
    {
        // Check if the dog is close enough to the food (within a certain threshold)
        if (Vector3.Distance(transform.position, targetBall.position) <= navAgent.stoppingDistance)
        {
            // Stop the movement
            navAgent.isStopped = true;
            Debug.Log("Moved");
            // Trigger the eating animation
            StartTakingBall(targetBall);
        }
    }

    void StartTakingBall(Transform ball)
    {
        if (!takeBall)
        {
            takeBall = true;
            Debug.Log("Dog found ball!");
            dogAnimator.SetFloat("TakingBallSpeed", 0);
            // Trigger the "Eat" animation
            dogAnimator.SetTrigger("TakingBall");
            StartCoroutine(WaitAndSnap(ball.gameObject));
        }
    }
        
    private IEnumerator WaitAndSnap(GameObject BallObject)
    {
        yield return new WaitForSeconds(3f);
        BallObject.GetComponent<Rigidbody>().useGravity = false;

        BallObject.GetComponent<Collider>().isTrigger = true;
        // Snap the ball to the dog's mouth position and rotation (world space)
        BallObject.transform.SetParent(mouthTransform);
        BallObject.transform.localPosition = Vector3.zero;
        BallObject.transform.localRotation = Quaternion.identity;
        navAgent.isStopped = false;
        // Move to player
        GameObject player = GameObject.Find("XR Origin");
        Debug.Log("Player found");
        if (player != null)
        {
            // Offset 2 units in player's forward (Z) direction
            Vector3 offsetPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z + 0.5f);
            Debug.Log(offsetPosition + " Player position");
            // Move the dog to the offset position
            navAgent.SetDestination(offsetPosition);
            StartCoroutine(WaitUntilAtPlayer(offsetPosition));
            //StopAtPlayer(offsetPosition);
        }
    }
    void StopAtPlayer(Vector3 offsetPosition)
    {
        // Check if the dog is close enough to the food (within a certain threshold)
        if (Vector3.Distance(transform.position, offsetPosition) <= navAgent.stoppingDistance)
        {
            // Stop the movement
            navAgent.isStopped = true;
            Debug.Log("Moved");
            StartGivingBall(targetBall);
        }
    }
    private IEnumerator WaitUntilAtPlayer(Vector3 offsetPosition)
    {
        // Wait until the dog is close enough to the player
        while (Vector3.Distance(transform.position, offsetPosition) > navAgent.stoppingDistance + 0.1f)
        {
            dogAnimator.SetFloat("TakingBallSpeed", moveSpeed);
            yield return null; // Wait for the next frame
        }

        navAgent.isStopped = true;
        Debug.Log("Moved to player");
        StartGivingBall(targetBall);
    }
    void StartGivingBall(Transform ball)
    {
        Debug.Log("Dog gave ball!");
        dogAnimator.SetFloat("TakingBallSpeed", 0);
        // Trigger the "Eat" animation
        dogAnimator.SetTrigger("Sit");
        // Unparent the ball
        ball.SetParent(null);

        // Drop the ball on the ground in front of the dog
        Vector3 dropPosition = transform.position + transform.forward * 0.5f;
        dropPosition.y = 0.2f; // Adjust height so it doesn't clip into floor
        ball.position = dropPosition;

        // Re-enable physics
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.velocity = Vector3.zero; // Optional: Stop any leftover movement
        }

        // Re-enable collision
        Collider col = ball.GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = false;
        }
    }
    public void DogStandUp()
    {
        if (takeBall)
        {
            dogAnimator.SetTrigger("Continue");
        }
    }
    void Jump()
    {
        if (gameObject.transform.position.y > 0.1f)
        {
            dogAnimator.SetFloat("Speed", 0);
            dogAnimator.SetTrigger("Jump");
        }
    }
}
