using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Trimming : MonoBehaviour
{
    public GameObject invisibleParicle;
    ParticleSystem particleFlow;
    void Start()
    {
        XRGrabInteractable grabbable2 = GetComponent<XRGrabInteractable>();

        // Listen for both activated and deactivated events
        grabbable2.activated.AddListener(StartTrim);
        grabbable2.deactivated.AddListener(Stop);

        particleFlow = invisibleParicle.GetComponent<ParticleSystem>();
    }

    void StartTrim(ActivateEventArgs arg)
    {
        invisibleParicle.SetActive(true);
        particleFlow.Play();
    }
    public void Stop(DeactivateEventArgs arg)
    {
        particleFlow.Stop();
        invisibleParicle.SetActive(false);  // You can also disable the water GameObject if needed
    }
}
