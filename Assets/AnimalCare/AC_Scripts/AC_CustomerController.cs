using UnityEngine;
using UnityEngine.AI;

public class AC_CustomerController : MonoBehaviour
{
    public Transform targetPoint; // Set by manager
    public Animator animator;

    private NavMeshAgent agent;
    private bool hasArrived = false;
    private AudioSource talk;
    public bool IsReadyToOrder => hasArrived;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        talk = GetComponent<AudioSource>();
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
            talk.Play();
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
