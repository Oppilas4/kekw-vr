using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_BatteryCompartment : MonoBehaviour
{
    Elec_TVremote PapaRemote;
    bool Occupied = false;
    XRBaseInteractable interactable;
    private void Start()
    {
        PapaRemote = gameObject.transform.parent.GetComponent<Elec_TVremote>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<XRBaseInteractable>() != null)
        {
            other.gameObject.GetComponent<XRBaseInteractable>().enabled = false;
        }
        if (other.tag == "Battery" && !Occupied)
        {
            other.enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.parent = gameObject.transform;
            other.gameObject.transform.position = gameObject.transform.position;
            other.gameObject.transform.rotation = gameObject.transform.rotation;
            Occupied = true;
        }
        PapaRemote.Batteries++;
    }
}
