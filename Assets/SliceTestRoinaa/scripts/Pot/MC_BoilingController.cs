using System;
using UnityEngine;

public class MC_BoilingController : MonoBehaviour
{
    public MC_PotController potController; // Drag the pot controller object in Inspector
    public GameObject timerObject; // timerObject is a child of the "Pot" object. get the reference by searching for a child called "CookingTimer"
    public MC_Timer timerScript; // timerScript is attached to the timerObject. get the reference by getting the component out of the timerObject

    public static event Action<VegetableController> OnVegetableBoiled;
    private void OnTriggerEnter(Collider other)
    {
        // Assuming the pot tag is "Pot"
        if (other.CompareTag("Pot"))
        {
            // Get the MC_PotController script from the pot object
            potController = other.GetComponent<MC_PotController>();

            // Check if the pot is filled and if the water is boiling
            if (potController != null && potController.isPotFilled && potController.boilingParticles.isPlaying)
            {
                // Find the timerObject as a child of the pot object
                timerObject = other.transform.Find("CookingTimer").gameObject;

                // Check if timerObject is found before attempting to access its components
                if (timerObject != null)
                {
                    // Get the MC_Timer script component from the timerObject
                    timerScript = timerObject.GetComponent<MC_Timer>();

                    // Check if timerScript is not null before starting the timer
                    if (timerScript != null)
                    {
                        timerScript.TimerComplete.AddListener(OnBoilComplete);
                        // Activate the timer object and start the timer
                        timerObject.SetActive(true);
                        timerScript.StartTimer(10); // Replace 10 with the desired duration in seconds
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Assuming the pot tag is "Pot"
        if (other.CompareTag("Pot"))
        {
            // Disable the timerObject when exiting the trigger
            if (timerObject != null)
            {
                timerScript.TimerComplete.RemoveListener(OnBoilComplete);

                timerObject.SetActive(false);
            }
        }
    }

    private void OnBoilComplete()
    {
        // Assuming you have a reference to the VegetableController that was boiled
        VegetableController boiledVegetableController = GetComponent<VegetableController>(); // Retrieve the VegetableController somehow

        // Raise the event to notify that the vegetable has been boiled
        OnVegetableBoiled?.Invoke(boiledVegetableController);

        print("Boiling Complete");
    }
}
