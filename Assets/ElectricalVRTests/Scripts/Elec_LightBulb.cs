using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_LightBulb : MonoBehaviour
{
    ParticleSystem LightParticle;
    public Light BulbLight;
    // Start is called before the first frame update
    void Start()
    {
        LightParticle = GetComponentInChildren<ParticleSystem>();
        LightParticle.Stop();
        BulbLight.enabled = false;
    }
    public void PuzzleComplete()
    {
        LightParticle.Play();
        BulbLight.enabled=true;
    }
}
