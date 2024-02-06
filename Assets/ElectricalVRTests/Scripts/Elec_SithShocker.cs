using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_SithShocker : MonoBehaviour
{
    XRBaseInteractable interactable;
    public XRDirectInteractor LeftHand,RightHand;
    GameObject ZappingHand;
    public ParticleSystem SithParticles;
    bool Shocking = false;
    public Elec_DeathItself Bob;
    [Obsolete]
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.onSelectEntered.AddListener(TookTheWireEnd);
        interactable.onSelectExited.AddListener(LmaoYouDied);
        LeftHand = GameObject.FindGameObjectWithTag("LeftHand").GetComponent<XRDirectInteractor>();
        RightHand = GameObject.FindGameObjectWithTag("RightHand").GetComponent<XRDirectInteractor>();
        SithParticles = GameObject.Find("SithLightning").GetComponent<ParticleSystem>();
        Bob = GameObject.FindObjectOfType<Elec_DeathItself>();
    }
    void Update()
    {
        if (Shocking)
        {
            SithParticles.transform.position = ZappingHand.transform.position;
            SithParticles.transform.rotation = ZappingHand.transform.rotation;
        }
    }
    void TookTheWireEnd(XRBaseInteractor interactor)
    {
        SithParticles.Play();
        Shocking = true;
        if (interactor.GetComponent<XRDirectInteractor>() != null)
        {
            if (interactor.tag == "LeftHand")
            {
                ZappingHand = RightHand.gameObject;
            }
            else if (interactor.tag == "RightHand")
            {
                ZappingHand = LeftHand.gameObject;
            }
        }
        StartCoroutine(TimeTillBobComes());
    }
    void LmaoYouDied(XRBaseInteractor interactor)
    {
        SithParticles.Stop();
        Shocking = false;
    }
    IEnumerator TimeTillBobComes()
    {
        yield return new WaitForSeconds(5);
        Bob.HereComesTheDeath();
    }
}
