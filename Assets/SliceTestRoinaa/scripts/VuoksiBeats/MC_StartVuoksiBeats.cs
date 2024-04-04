using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Obsolete]
public class MC_StartVuoksiBeats : MonoBehaviour
{
    public XRGrabInteractable sword1;
    public XRGrabInteractable sword2;

    private bool sword1Grabbed = false;
    private bool sword2Grabbed = false;

    void Start()
    {
        // Subscribe to the OnSelectEntered and OnSelectExited events for both swords
        sword1.onSelectEntered.AddListener(OnSword1Grabbed);
        sword1.onSelectExited.AddListener(OnSword1Released);
        sword2.onSelectEntered.AddListener(OnSword2Grabbed);
        sword2.onSelectExited.AddListener(OnSword2Released);
    }

    void OnSword1Grabbed(XRBaseInteractor interactor)
    {
        sword1Grabbed = true;
        CheckBothSwordsGrabbed();
    }

    void OnSword1Released(XRBaseInteractor interactor)
    {
        sword1Grabbed = false;
        StopGame();
    }

    void OnSword2Grabbed(XRBaseInteractor interactor)
    {
        sword2Grabbed = true;
        CheckBothSwordsGrabbed();
    }

    void OnSword2Released(XRBaseInteractor interactor)
    {
        sword2Grabbed = false;
        StopGame();
    }

    void CheckBothSwordsGrabbed()
    {
        if (sword1Grabbed && sword2Grabbed)
        {
            StartGame(); // Call your method to start the game
        }
    }

    void StartGame()
    {
        MC_VuoksiBeatsSpawner spawner = FindAnyObjectByType<MC_VuoksiBeatsSpawner>();
        spawner.StartVuoksiBeats();
    }

    void StopGame()
    {
        MC_VuoksiBeatsSpawner spawner = FindAnyObjectByType<MC_VuoksiBeatsSpawner>();
        spawner.StopVuoksiBeats();
    }
}
