using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Elec_CofffeeFlakes : MonoBehaviour
{
    bool flaked = false;
    public AudioSource TeroNomNom;
    Rigidbody Rigid;
    public float vel;
    Elec_KahviFlakesParticles KahviFlakesParticles;
    private void Start()
    {
        KahviFlakesParticles = GetComponentInChildren<Elec_KahviFlakesParticles>();
        Rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0.75 && !flaked && Rigid.velocity.y < -vel)
        {
            flaked = true;
            KahviFlakesParticles.PartIt();
        }
        if (Vector3.Dot(transform.up, Vector3.down) < 0.75 && flaked)
        {
            flaked = false;
        }
    }
}