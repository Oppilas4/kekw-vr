using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_LightBulb : MonoBehaviour
{
    ParticleSystem LightParticle;
    public Material EmissionGreen;
    Material Glass;
    MeshRenderer LightMesh;
    // Start is called before the first frame update
    void Start()
    {
        LightMesh = GetComponent<MeshRenderer>();
        LightParticle = GetComponentInChildren<ParticleSystem>();
        Glass = LightMesh.material;
        LightParticle.Stop();
    }
    public void BulbEnablee()
    {
        LightMesh.material = EmissionGreen;
        LightParticle.Play();
    }
    public void BulbDisable()
    {
        LightMesh.material = Glass;
        LightParticle.Stop();
    }
}
