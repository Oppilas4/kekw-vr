using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_AnimOnTrigger : MonoBehaviour
{
    [SerializeField] float howLongFromStart = 5f;
    [SerializeField] bool startsAtStart = false;
    [SerializeField] float howLongFromTriggerEnter = 0f;
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip theClip;

    [SerializeField] string triggerName;

    [SerializeField] bool willHappenOnce = false;
    bool hasHappened;
    bool isPlaying = false;
    [SerializeField] bool needsToBePlayer = true;

    private IEnumerator Start()
    {
        if(startsAtStart)
        {
            yield return new WaitForSeconds(howLongFromStart);
            isPlaying = true;
            animator.SetTrigger(triggerName);
            yield return new WaitForSeconds(theClip.length);
            Debug.Log("Finsihed THe Animation");

            isPlaying = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(needsToBePlayer && other.gameObject.CompareTag("Player"))
        {
            CheckAThing();
        }
        else if(!needsToBePlayer)
        {
            CheckAThing();
        }
    }

    void CheckAThing()
    {
        if (!hasHappened && willHappenOnce && !isPlaying)
        {
            StartCoroutine(thingToCall());
        }
        else if (!willHappenOnce && !isPlaying)
        {
            StartCoroutine(thingToCall());
        }
    }

    private IEnumerator thingToCall()
    {
        yield return new WaitForSeconds(howLongFromTriggerEnter);
        isPlaying = true;
        animator.SetTrigger(triggerName);

        yield return new WaitForSeconds(theClip.length);
        Debug.Log("Finsihed THe Animation");

        isPlaying = false;
        hasHappened = true;
    }
}
