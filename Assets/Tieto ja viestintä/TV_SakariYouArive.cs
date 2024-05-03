using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TV_SakariYouArive : MonoBehaviour
{
    bool hasStarted = false;
    [SerializeField] TV_SakariTalk talk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!hasStarted)
            {
                talk.ActivateHeyYou();
                hasStarted = true;
                StartCoroutine(ResetHasStarted());
            }
        }
    }

    IEnumerator ResetHasStarted()
    {
        yield return new WaitForSeconds(20f); // Wait for 20 seconds
        hasStarted = false; // Reset hasStarted flag
    }
}
