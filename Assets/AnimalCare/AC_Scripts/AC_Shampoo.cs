using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Shampoo : MonoBehaviour
{
    public GameObject shampooliquid;
    ParticleSystem liquidParticle;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable2 = GetComponent<XRGrabInteractable>();

        // Listen for both activated and deactivated events
        grabbable2.activated.AddListener(PourShampoo);
        grabbable2.deactivated.AddListener(StopPouringShampoo);

        liquidParticle = shampooliquid.GetComponent<ParticleSystem>();
    }

    // This function is called when the grab button is pressed
    public void PourShampoo(ActivateEventArgs arg)
    {
        shampooliquid.SetActive(true);
        liquidParticle.Play();
    }

    // This function is called when the grab button is released
    public void StopPouringShampoo(DeactivateEventArgs arg)
    {
        liquidParticle.Stop();
        shampooliquid.SetActive(false);  // You can also disable the water GameObject if needed
    }
}
