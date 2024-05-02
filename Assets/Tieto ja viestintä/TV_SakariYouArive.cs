using System.Collections;
using UnityEngine;

public class TV_SakariYouArive : MonoBehaviour
{
    bool hasStarted = false;
    [SerializeField] TV_SakariTalk talk;
    [SerializeField] AudioSource sakariAudio;

    private void Start()
    {
        sakariAudio.dopplerLevel = .1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sakariAudio.dopplerLevel = 1f;
            if(!hasStarted)
            {
                talk.ActivateHeyYou();
                hasStarted = true;
                StartCoroutine(ResetHasStarted());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sakariAudio.dopplerLevel = .1f;
        }
    }

    IEnumerator ResetHasStarted()
    {
        yield return new WaitForSeconds(20f); // Wait for 20 seconds
        hasStarted = false; // Reset hasStarted flag
    }
}
