using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SandItem : MonoBehaviour
{
    XRBaseInteractor interactor;
    public GameObject WhatToSpawn;
    private void Start()
    {
        interactor = GetComponent<XRBaseInteractor>();
        interactor.startingSelectedInteractable = Instantiate(WhatToSpawn,this.transform.position,this.transform.rotation).GetComponent<XRBaseInteractable>();
        interactor.onSelectExited.AddListener(SpawnItem);
    }

    private void SpawnItem(XRBaseInteractable arg0)
    {
        Instantiate(WhatToSpawn, this.transform.position, this.transform.rotation);
    }
}
