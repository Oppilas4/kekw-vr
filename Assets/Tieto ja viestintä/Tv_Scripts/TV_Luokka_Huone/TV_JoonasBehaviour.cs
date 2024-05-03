using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TV_JoonasBehaviour : MonoBehaviour
{
    private Tv_JoonasAudioManager audioManager;
    public Transform sitLocation;
    public Transform whereToGoStand;
    public AudioClip[] sittingQuotes;
    public AudioClip standingQuote;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] Tv_JoonasActivation activation;
    [SerializeField] GameObject teleportPortal;

    private List<AudioClip> sittingQuotesQueue = new List<AudioClip>();
    private int currentQuoteIndex = 0;
    public bool sitting;
    bool hasStartedCoroutine = false;

    void Start()
    {
        audioManager = GetComponent<Tv_JoonasAudioManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Sit();

        // Shuffle the sittingQuotes array
        ShuffleArray(sittingQuotes);
        // Populate the sittingQuotesQueue
        sittingQuotesQueue.AddRange(sittingQuotes);

        StartCoroutine(PlayRandomQuoteRoutine());
    }

    private void Update()
    {
        ReachedTheDestination();
    }

    IEnumerator PlayRandomQuoteRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            if (animator.GetBool("IsSitting"))
            {
                if (sittingQuotesQueue.Count > 0)
                {
                    // Play the current quote
                    audioManager.PlayVoiceline(sittingQuotesQueue[currentQuoteIndex]);
                    /*audioSource.clip = sittingQuotesQueue[currentQuoteIndex];
                    audioSource.Play();
                    */
                    // Move to the next quote
                    currentQuoteIndex++;
                    if (currentQuoteIndex >= sittingQuotesQueue.Count)
                    {
                        // Reshuffle the array and reset the index
                        ShuffleArray(sittingQuotes);
                        sittingQuotesQueue.Clear();
                        sittingQuotesQueue.AddRange(sittingQuotes);
                        currentQuoteIndex = 0;
                    }
                }
            }
        }
    }

    void Sit()
    {
        sitting = true;
        transform.position = sitLocation.position;
        transform.rotation = sitLocation.rotation;
        agent.enabled = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsSitting", true);
        activation.hasReachedDesk = true;
    }

    public void StartToMove(Transform point)
    {
        sitting = false;
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsSitting", false);
        animator.SetBool("IsIdle", false);

        agent.enabled = true;
        agent.SetDestination(point.position);
    }

    public void ReachedTheDestination()
    {
        if (agent.enabled == true)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (Vector3.Distance(transform.position, sitLocation.position) < 0.5f)
                {
                    Sit();
                }
                else if (Vector3.Distance(transform.position, whereToGoStand.position) < 0.5f)
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsSitting", false);
                    animator.SetBool("IsIdle", true);

                    // Play standing quote
                    if(!hasStartedCoroutine)
                    {
                        StartCoroutine(koikeliMoikeli());
                    }
                }
            }
        }
    }

    void ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = Random.Range(i, array.Length);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }


    IEnumerator koikeliMoikeli()
    {
        hasStartedCoroutine = true;
        audioManager.PlayVoiceline(standingQuote);
        /*audioSource.clip = standingQuote;
        audioSource.Play();
        */
        yield return new WaitForSeconds(9f);
        teleportPortal.SetActive(true);
        JoonasReturn();
    }
    void JoonasReturn()
    {
        StartToMove(sitLocation);
        hasStartedCoroutine = false;
    }
}