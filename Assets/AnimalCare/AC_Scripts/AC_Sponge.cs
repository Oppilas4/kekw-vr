using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Sponge : MonoBehaviour
{
    public GameObject foam;
    ParticleSystem foamParticle;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable2 = GetComponent<XRGrabInteractable>();

        // Listen for both activated and deactivated events
        grabbable2.activated.AddListener(MakeFoam);
        grabbable2.deactivated.AddListener(StopFoam);

        foamParticle = foam.GetComponent<ParticleSystem>();
    }

    // This function is called when the grab button is pressed
    public void MakeFoam(ActivateEventArgs arg)
    {
        foam.SetActive(true);
        foamParticle.Play();
    }

    // This function is called when the grab button is released
    public void StopFoam(DeactivateEventArgs arg)
    {
        foamParticle.Stop();
        foam.SetActive(false);  // You can also disable the water GameObject if needed
    }
}
