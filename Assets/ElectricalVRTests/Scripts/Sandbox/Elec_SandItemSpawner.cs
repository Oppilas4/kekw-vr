using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SandItemSpawner : MonoBehaviour
{
    XRBaseInteractor interactor;
    public GameObject WhatToSpawn;
    [Obsolete]
    private void Start()
    {
        gameObject.SetActive(true);
        interactor = GetComponent<XRBaseInteractor>();
        Instantiate(WhatToSpawn, transform.position, transform.rotation);
    }

    public void SpawnItem()
    {      
        GameObject TempItem = Instantiate(WhatToSpawn, transform.position, transform.rotation);
    }
}
