using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic; // Make sure to include this namespace for List

public class MC_DisableRayInteractor : MonoBehaviour
{
    private List<XRRayInteractor> interactors = new List<XRRayInteractor>();

    private void Start()
    {
        XRRayInteractor[] rayInteractors = FindObjectsOfType<XRRayInteractor>();
        foreach (XRRayInteractor interactor in rayInteractors)
        {
            interactors.Add(interactor);
        }
        DisableAllRayInteractors();
    }

    public void DisableAllRayInteractors()
    {
        foreach (XRRayInteractor interactor in interactors)
        {
            interactor.enabled = false;
        }
    }

    public void EnableAllRayInteractors()
    {
        foreach (XRRayInteractor interactor in interactors)
        {
            interactor.enabled = true;
        }
    }
}
