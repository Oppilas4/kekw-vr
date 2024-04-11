using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Elec_CofffeeFlakes : MonoBehaviour
{
    ParticleSystem CoffeeFlakes;
    bool flaked = false;
    public AudioSource TeroNomNom;
    Rigidbody Rigid;
    public float vel;
    private void Start()
    {
        CoffeeFlakes = GetComponent<ParticleSystem>();
        Rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0.75 && !flaked && Rigid.velocity.y < -vel)
        {
            CoffeeFlakes.Play();
            flaked = true;
        }
        if (Vector3.Dot(transform.up, Vector3.down) < 0.75 && flaked)
        {
            flaked = false;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "elec_tero")
        {
            TeroNomNom.Play();
        }
    }
}