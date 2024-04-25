using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_EcoloopButton : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToActivate;
    [SerializeField] GameObject[] objectsToDeactivate;

    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            ToggleActivation();
        }
    }

    private void ToggleActivation()
    {
        // Toggle the activation state
        isActivated = !isActivated;

        // Activate or deactivate objects based on the current state
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(isActivated);
        }

        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(!isActivated);
        }
    }
}
