using System.Collections;
using UnityEngine;

public class TV_OttoNaytto : MonoBehaviour
{
    [SerializeField] Texture mouthClosed, mouthOpen, blueScreen;
    [SerializeField] Material theMat;
    [SerializeField] AudioSource audioSa;
    bool hasSpoken = false;

    private void Start()
    {
        theMat.mainTexture = mouthClosed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpoken)
        {
            StartCoroutine(SpeakingAnimation());
        }
    }

    IEnumerator SpeakingAnimation()
    {
        hasSpoken = true; // Set to true to prevent re-triggering while speaking

        audioSa.Play();

        // Switch mouth texture every 0.15 seconds until audio clip finishes
        while (audioSa.isPlaying)
        {
            theMat.mainTexture = mouthOpen;
            yield return new WaitForSeconds(0.15f);

            theMat.mainTexture = mouthClosed;
            yield return new WaitForSeconds(0.15f);
        }

        // Set mouth to closed after audio clip ends
        theMat.mainTexture = mouthClosed;

        // Wait for 10 seconds
        yield return new WaitForSeconds(5f);

        // Apply blue screen texture
        theMat.mainTexture = blueScreen;
    }
}
