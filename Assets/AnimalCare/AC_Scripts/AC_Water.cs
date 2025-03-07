using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Water : MonoBehaviour
{
    public GameObject water;
    ParticleSystem waterFlow;
    public bool waterRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable2 = GetComponent<XRGrabInteractable>();

        // Listen for both activated and deactivated events
        grabbable2.activated.AddListener(PourWater);
        grabbable2.deactivated.AddListener(StopWater);

        waterFlow = water.GetComponent<ParticleSystem>();
    }

    // This function is called when the grab button is pressed
    public void PourWater(ActivateEventArgs arg)
    {
        waterRunning = true;
        water.SetActive(true);
        waterFlow.Play();
    }

    // This function is called when the grab button is released
    public void StopWater(DeactivateEventArgs arg)
    {
        waterRunning = false;
        waterFlow.Stop();
        water.SetActive(false);  // You can also disable the water GameObject if needed
    }
}
