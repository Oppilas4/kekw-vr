using SerializableCallback;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Elec_SandBoxItem : MonoBehaviour
{
    Elec_SandItemSpawner MamaSpawner;
    XRBaseInteractable interactable;
    public float DistanceToDetach;
    public Vector3 OffsetToSandBox;
    public Quaternion SandBoxRotation;
    public List<Elec_SandBoxInOut> Input;
    public List<Elec_SandBoxInOut> Output;
    public int Voltage = 0;
    public UnityEvent WhenOn, WhenOff;
    bool locked = false;
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(MamaFind);
        interactable.selectEntered.AddListener(MamaSpawn);
        StartCoroutine(WaitTillOn());
    }
    void MamaFind(SelectEnterEventArgs args)
    {
        if (MamaSpawner == null) MamaSpawner = args.interactorObject.transform.GetComponent<Elec_SandItemSpawner>();
    }
    void MamaSpawn(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.GetComponent<XRDirectInteractor>() != null && MamaSpawner != null || args.interactorObject.transform.GetComponent<XRRayInteractor>() != null && MamaSpawner != null)
        {
            MamaSpawner.SpawnItem();
            MamaSpawner = null;
        }
    }
    IEnumerator WaitTillOn()
    {
        yield return new WaitUntil(() => Voltage > 0);
        WhenOn.Invoke();
        StartCoroutine(WaitTillOff());
    }
    IEnumerator WaitTillOff()
    {
        yield return new WaitUntil(() => Voltage <= 0);
        WhenOff.Invoke();
        StartCoroutine(WaitTillOn());
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
        transform.position = new Vector3(pos.x + OffsetToSandBox.x, transform.position.y, transform.position.z);

    }
    public void BackToNormal()
    {
        locked = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}