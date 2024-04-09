using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SandBoxItem : MonoBehaviour
{
    Elec_SandItemSpawner MamaSpawner;
    XRBaseInteractable interactable;
    public float DistanceToDetach;
    public Vector3 OffsetToSandBox;
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
    private void Update()
    {
        if (interactable.isSelected)
        {
            var Interactor = interactable.interactorsSelecting[0];
            if (Interactor == null) return;
            else if (interactable.interactorsSelecting[0].transform.GetComponent<XRRayInteractor>() != null) return;
            else if (Vector3.Distance(interactable.interactorsSelecting[0].transform.position, transform.position) > DistanceToDetach)
            {
                transform.position = interactable.GetOldestInteractorSelecting().transform.position;
            }
        }               
    }
    public void PositionToBox(Vector3 pos)
    {
        transform.position = new Vector3(pos.x + OffsetToSandBox.x,transform.position.y,transform.position.z);
    }
}