using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elec_ballhitfx : MonoBehaviour
{
    public ParticleSystem ourParticles;
    public void OnCollisionEnter(Collision collision)
    {
        ourParticles.Play();
    }
}
