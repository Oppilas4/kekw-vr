using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TV_JoonasBehaviour : MonoBehaviour
{

    public GameObject buttonActive;
    public Transform sitLocation;
    public Transform whereToGoStand;
    public AudioClip[] sittingQuotes;
    public AudioClip standingQuote;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;

    private List<AudioClip> sittingQuotesQueue = new List<AudioClip>();
    private int currentQuoteIndex = 0;
    public bool sitting;

    void Start()
    {
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
                    audioSource.clip = sittingQuotesQueue[currentQuoteIndex];
                    audioSource.Play();
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
        buttonActive.SetActive(true);
        transform.position = sitLocation.position;
        transform.rotation = sitLocation.rotation;
        agent.enabled = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsSitting", true);
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
                    StartCoroutine(koikeliMoikeli());
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
        audioSource.clip = standingQuote;
        audioSource.Play();
        yield return new WaitForSeconds(8f);
        JoonasReturn();
    }
    void JoonasReturn()
    {
        StartToMove(sitLocation);
    }
}