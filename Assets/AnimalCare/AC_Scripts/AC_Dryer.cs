using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AC_Dryer : MonoBehaviour
{
    public GameObject wind;
    ParticleSystem windFlow;

    public AC_DogMaterialChance dog;
    public AC_ChecklistManager checklistManager;

    public float proximityThreshold = 1f;  // Set this to the distance at which the dog should be considered "near"

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable2 = GetComponent<XRGrabInteractable>();

        // Listen for both activated and deactivated events
        grabbable2.activated.AddListener(StartWind);
        grabbable2.deactivated.AddListener(StopWind);

        windFlow = wind.GetComponent<ParticleSystem>();
    }

    // This function is called when the grab button is pressed
    public void StartWind(ActivateEventArgs arg)
    {
        wind.SetActive(true);
        windFlow.Play();
        if (dog.wet2 && Vector3.Distance(transform.position, dog.transform.position) <= proximityThreshold)
        {
            checklistManager.CompleteTask(2);
            dog.ChangeColorBack();
        }
    }

    // This function is called when the grab button is released
    public void StopWind(DeactivateEventArgs arg)
    {
        windFlow.Stop();
        wind.SetActive(false);  // You can also disable the water GameObject if needed
    }
}
