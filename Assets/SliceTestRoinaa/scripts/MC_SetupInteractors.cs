using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MC_SetupInteractors : MonoBehaviour
{
    List<XRBaseInteractor> interactorsInScene;
    XRInteractionManager interactorManager;
    private void Start()
    {
        interactorsInScene = FindObjectsOfType<XRBaseInteractor>().ToList();
        interactorManager = GetComponent<XRInteractionManager>();
        foreach (XRBaseInteractor interactor in interactorsInScene)
        {
            interactor.interactionManager = interactorManager;
        }
    }
}
