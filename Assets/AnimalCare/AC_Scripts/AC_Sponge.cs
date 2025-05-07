using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Sponge : MonoBehaviour
{
    public GameObject foam;
    ParticleSystem foamParticle;
    public bool foamRunning = false;
    public bool hasShampoo = false;
    // Start is called before the first frame update
    void Start()
    {
        foamParticle = foam.GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Shampoo"))
        {
            MakeFoam();
        }
    }
    // This function is called when the grab button is pressed
    public void MakeFoam()
    {
        foamRunning = true;
        foam.SetActive(true);
        foamParticle.Play();
    }

    // This function is called when the grab button is released
    public void StopFoam()
    {
        foamRunning = false;
        foamParticle.Stop();
        foam.SetActive(false);  // You can also disable the water GameObject if needed
    }
}
