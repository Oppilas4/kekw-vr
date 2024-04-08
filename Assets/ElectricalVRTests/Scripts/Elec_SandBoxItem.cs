using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SandBoxItem : MonoBehaviour
{
    Elec_SandItemSpawner MamaSpawner;
    XRBaseInteractable interactable;
    [Obsolete]
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();  
        interactable.onSelectEntered.AddListener(MamaFind);
        interactable.onSelectEntered.AddListener(MamaSpawn);
    }
    void MamaFind(XRBaseInteractor interactor)
    {
        if(MamaSpawner == null) MamaSpawner = interactor.GetComponent<Elec_SandItemSpawner>();
    }
    void MamaSpawn(XRBaseInteractor xRBaseInteractor)
    {
        if (xRBaseInteractor.GetComponent<XRDirectInteractor>() != null && MamaSpawner != null  || xRBaseInteractor.GetComponent<XRRayInteractor>() != null && MamaSpawner != null)
        {
            MamaSpawner.SpawnItem();
            MamaSpawner = null;
        }
    }
}
