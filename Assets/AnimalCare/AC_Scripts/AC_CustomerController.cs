using UnityEngine;
using UnityEngine.AI;

public class AC_CustomerController : MonoBehaviour
{
    public Transform targetPoint; // Set by manager
    public Animator animator;

    private NavMeshAgent agent;
    private bool hasArrived = false;

    public bool IsReadyToOrder => hasArrived;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (targetPoint == null)
        {
            Debug.LogWarning("No target point set for customer!");
            return;
        }

        agent.SetDestination(targetPoint.position);
        animator.SetTrigger("Walk");
    }

    void Update()
    {
        if (!hasArrived && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            hasArrived = true;
            agent.isStopped = true;
            animator.SetTrigger("Stop");
            StartCoroutine(TalkForSeconds(3f));
        }
    }

    System.Collections.IEnumerator TalkForSeconds(float seconds)
    {
        animator.SetTrigger("Talk");
        yield return new WaitForSeconds(seconds);
        animator.SetTrigger("StopTalk");
    }
}
