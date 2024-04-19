using OVRSimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour, IDial
{
    public bool isOn = false;
    [SerializeField] ParticleSystem flameParticles;
    public AudioSource _audioSource;

    void Start()
    {
        var emission = flameParticles.emission;
        emission.enabled = isOn;
    }

    public void DialChanged(float dialValue)
    {
        var lifetimeMultiplier = Mathf.Clamp(dialValue / 360f, 0, 0.1f);
        var main = flameParticles.main;
        main.startLifetimeMultiplier = lifetimeMultiplier;

        isOn = lifetimeMultiplier > 0.02f;

        // Enable or disable the particle system based on whether the stove is on
        var emission = flameParticles.emission;
        emission.enabled = isOn;

        if (isOn)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

    public bool GetIsOn()
    {
        return isOn;
    }
}
