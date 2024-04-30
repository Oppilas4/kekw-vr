using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TV_PirateCannon : MonoBehaviour
{
    public ParticleSystem firstParticle;
    public ParticleSystem secondParticle;
    public GameObject cannonBall;
    public GameObject first;
    public GameObject secon;

    public float startDelay = 0f; // Initial delay before starting the loop
    public float firstParticleDelay = 5f; // Delay before playing the first particle
    public float secondParticleDelay = 1f; // Delay before playing the second particle

    void Start()
    {
        StartCoroutine(PlayParticlesLoop(startDelay));
    }

    IEnumerator PlayParticlesLoop(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            yield return new WaitForSeconds(firstParticleDelay);
            secon.SetActive(false);
            first.SetActive(true);
            cannonBall.SetActive(true);
            yield return new WaitForSeconds(secondParticleDelay);
            secon.SetActive(true);
            cannonBall.SetActive(false);
            first.SetActive(false);
        }
    }
}
