using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Soap : MonoBehaviour
{
    public GameObject soap;
    public AC_Sponge sponge;
   
    public bool foamed = false;
    ParticleSystem soapParticle;
    public AC_DogWashing wetdog;
    public AudioSource foamSound;
    void Start()
    {
        soapParticle = soap.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (wetdog.wet2)
        {
            soapParticle.Stop();
            soap.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Sponge" && sponge.foamRunning && wetdog.wet1)
        {
            soap.SetActive(true);
            soapParticle.Play();
            
            foamed = true;
            sponge.StopFoam();
            foamSound.Play();
        }
    }
}
