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
        CoffeeFlakes = GetComponentInChildren<ParticleSystem>();
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
}