using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_LightBulb : MonoBehaviour
{
    ParticleSystem LightParticle;
    Light BulbLight;
    // Start is called before the first frame update
    void Start()
    {
        LightParticle = GetComponentInChildren<ParticleSystem>();
        BulbLight = GetComponentInChildren<Light>();
        LightParticle.Stop();
        BulbLight.enabled = false;
    }
    public void BulbEnablee()
    {
        LightParticle.Play();
        BulbLight.enabled=true;
    }
    public void BulbDisable()
    {
        LightParticle.Stop();
        BulbLight.enabled = false;
    }
}
