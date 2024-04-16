using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField] Animator anim;
    [SerializeField] Transform sakariLookHere;

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

    void Update()
    {
        switch (currentState)
        {
            case SakariState.Idle:
                UpdateIdleState();
                break;
            case SakariState.Throwing:
                break;
            case SakariState.Returning:
                UpdateReturningState();
                break;
        }
    }
    void UpdateReturningState()
    {
        navMeshAgent.SetDestination(startPos.position);

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f))
        {
            currentState = SakariState.Idle;
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsWalk", false);
            transform.rotation = sakariLookHere.rotation;
        }
    }

    void UpdateIdleState()
    {
        if (!carryingBottle && !sakariAnger)
        {
            if (isBottle)
            {
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsWalk", true);
                navMeshAgent.SetDestination(bottle.transform.position);
            }
        }
        else if (!sakariAnger)
        {
            navMeshAgent.SetDestination(door.position);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalk", true);

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f))
            {
                currentState = SakariState.Throwing;
                anim.SetBool("IsIdle", true);
                anim.SetBool("IsWalk", false);
                Invoke("ThrowBottleOut", 1.0f);
            }

            Vector3 lookDirection = (door.position - transform.position).normalized;
            lookDirection.y = 0f;

            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
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
        rb.isKinematic = true;
        bottle.transform.parent = attachTransformHand;
        bottle.transform.localPosition = Vector3.zero;
        bottle.transform.localRotation = Quaternion.identity;
    }

    void SakariReturn()
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalk", true);
        navMeshAgent.speed = 1f;
        navMeshAgent.SetDestination(startPos.position);
    }
    void ThrowBottleOut()
    {
        sakariAnger = true;
        carryingBottle = false;
        Rigidbody rb = bottle.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        bottle.transform.parent = null;
        Vector3 throwDirection = (door.position - throwTransform.position).normalized;
        Vector3 totalForce = throwDirection * throwForce + Vector3.up * upForce;
        rb.AddForce(totalForce, ForceMode.Impulse);
        float torqueStrength = 0.5f;
        Vector3 torqueDirection = Random.onUnitSphere;
        rb.AddTorque(torqueDirection * torqueStrength, ForceMode.Impulse);
        currentState = SakariState.Returning;
        Invoke("SakariReturn", 1.0f);
    }
}
