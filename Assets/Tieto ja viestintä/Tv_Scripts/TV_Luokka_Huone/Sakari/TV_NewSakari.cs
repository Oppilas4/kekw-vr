using Kekw.VuoksiBotti;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TV_NewSakari : MonoBehaviour
{
    public List<GameObject> bottlesToThrow;
    public GameObject currentBottle;


    [SerializeField] Transform whereBottleGoesWhenPicked, theDoor, theDesk, throwPoint;
    [SerializeField] TV_SakariTalk talk;
    private Animator anim;
    private NavMeshAgent navMeshAgent;
    private bool hasBottleEquiped = false;
    private bool isGoingToThrow = false;
    private bool hasThrow = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(bottlesToThrow.Count > 0 && !hasBottleEquiped && !hasThrow)
        {
            FollowFirstBottle();
        }
        else if(bottlesToThrow.Count <= 0 && currentBottle == null)
        {
            MoveTowardsWhat(theDesk);
        }

        else if(hasBottleEquiped && !isGoingToThrow)
        {
            MoveTowardsWhat(theDoor);
        }
        else if(isGoingToThrow)
        {
            StartCoroutine(StartToTrowh());
        }
        else if(!hasBottleEquiped && !isGoingToThrow && hasThrow)
        {
            MoveTowardsWhat(theDesk);
        }
    }

    void PickUpBottle(GameObject theBottle)
    {
        bottlesToThrow.Remove(theBottle);
        currentBottle = theBottle;
        Rigidbody rb = currentBottle.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        TV_SakariThrowObject throws = currentBottle.GetComponent<TV_SakariThrowObject>();
        throws.ShowTheMagic(true);
        throws.CheckIfInSakariHands(true);
        currentBottle.transform.parent = whereBottleGoesWhenPicked;
        currentBottle.transform.localPosition = Vector3.zero;
        currentBottle.transform.localRotation = Quaternion.identity;
        hasBottleEquiped = true;
    }

    void MoveTowardsWhat(Transform thePoint)
    {
        UpdateAnims(false);
        navMeshAgent.SetDestination(thePoint.transform.position);
        if(thePoint == theDesk)
        {
            CheckIfAtDesk();
        }
        else
        {
            CheckIfAtDoor();
        }

    }

    void CheckIfAtDoor()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Debug.Log("Reached the Door");
            isGoingToThrow = true;
        }
    }

    void CheckIfAtDesk()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Debug.Log("Reached the Desk");
            UpdateAnims(true);
            hasThrow = false;
        }
    }

    IEnumerator StartToTrowh()
    {
        Debug.Log("Started To Threw");
        UpdateAnims(true);
        yield return new WaitForSeconds(1f); // Wait for 1 second
        if (!hasThrow)
        {
            Throw();
            isGoingToThrow = false;
        }
        yield return new WaitForSeconds(1f); // Wait for 1 second after throwing
    }


    void Throw()
    {
        hasThrow = true;
        Debug.Log("Threw");
        Rigidbody rb = currentBottle.GetComponent<Rigidbody>();
        TV_SakariThrowObject throws = currentBottle.GetComponent<TV_SakariThrowObject>();
        throws.ShowTheMagic(false);
        throws.CheckIfInSakariHands(false);
        rb.isKinematic = false;
        currentBottle.transform.parent = null;
        Vector3 throwDirection = (theDoor.position - throwPoint.position).normalized;
        Vector3 totalForce = throwDirection * -150f + Vector3.up * 30f;
        rb.AddForce(totalForce, ForceMode.Impulse);
        float torqueStrength = 0.5f;
        Vector3 torqueDirection = Random.onUnitSphere;
        rb.AddTorque(torqueDirection * torqueStrength, ForceMode.Impulse);
        currentBottle = null;
        hasBottleEquiped = false;
    }

    void FollowFirstBottle()
    {
        UpdateAnims(false);
        talk.ActivateBottle();
        GameObject firstBottle = bottlesToThrow[0];
        navMeshAgent.SetDestination(firstBottle.transform.position);
    }

    void UpdateAnims(bool whatToApply)
    {
        anim.SetBool("IsIdle", whatToApply);
        anim.SetBool("IsWalk", !whatToApply);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tv_pullo") && !hasBottleEquiped && !hasThrow)
        {
            TV_SakariThrowObject throwObject = other.GetComponent<TV_SakariThrowObject>();
            if(throwObject.isInHands == false)
            {
                PickUpBottle(other.gameObject);
            }
        }
    }
}
