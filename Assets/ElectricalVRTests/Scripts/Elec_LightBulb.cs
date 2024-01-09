using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_LightBulb : MonoBehaviour
{
    ParticleSystem LightParticle;
    
    // Start is called before the first frame update
    void Start()
    {
        LightParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
