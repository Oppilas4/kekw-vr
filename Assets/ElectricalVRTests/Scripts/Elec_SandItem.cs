using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SandItem : MonoBehaviour
{
    XRBaseInteractor interactor;
    public GameObject WhatToSpawn;

    [Obsolete]
    private void Start()
    {
        gameObject.SetActive(true);
        interactor = GetComponent<XRBaseInteractor>();
        interactor.onSelectExited.AddListener(SpawnItem);
    }

    private void SpawnItem(XRBaseInteractable arg0)
    {
        Instantiate(WhatToSpawn, transform.position, transform.rotation);
    }
}
