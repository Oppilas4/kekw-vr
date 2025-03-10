using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Soap : MonoBehaviour
{
    public GameObject soap;
    ParticleSystem soapParticle;
    void Start()
    {
        soapParticle = soap.GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Sponge")
        {
            Debug.Log("Sponge hit");
            soap.SetActive(true);
            soapParticle.Play();
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Sponge")
        {
            Debug.Log("Sponge out");
            soapParticle.Stop();
            soap.SetActive(false);
        }
    }
}
