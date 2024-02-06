using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SandBoxItem : MonoBehaviour
{
    public Elec_SandItemSpawner MamaSpawner;
    XRBaseInteractable interactable;
    void Start()
    {
        interactable.onSelectEntered.AddListener(MamaSpawn);
    }
    void MamaSpawn(XRBaseInteractor xRBaseInteractor)
    {
        if (xRBaseInteractor.GetComponent<XRDirectInteractor>() != null || xRBaseInteractor.GetComponent<XRRayInteractor>() != null)
        {
            MamaSpawner.SpawnItem();
        }
    }
}
