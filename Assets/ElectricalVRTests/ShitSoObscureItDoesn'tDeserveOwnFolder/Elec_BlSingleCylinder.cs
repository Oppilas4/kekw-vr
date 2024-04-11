using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_BlSingleCylinder : MonoBehaviour
{
    bool Occupied = false;
    GameObject Round;
    public Transform Barrel;
    public float Force;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.GetComponent<XRBaseInteractable>() != null && !Occupied && other.GetComponent<XRBaseInteractable>().isSelected)
        {
            other.GetComponent<XRBaseInteractable>().enabled = false;
            Round = other.gameObject;
            other.enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.parent = gameObject.transform;
            other.gameObject.transform.position = Barrel.transform.position;
            other.gameObject.transform.rotation = gameObject.transform.rotation;
            Occupied = true;
        }
    }
    public void Propell()
    {
        if (Round != null)
        {
            Round.GetComponent<XRBaseInteractable>().enabled = true;
            Round.transform.position = Barrel.position;
            Round.transform.parent = null;
            Round.GetComponent<Collider>().enabled = true;
            Round.GetComponent<Rigidbody>().isKinematic = false;
            Round.GetComponent<Rigidbody>().AddForce(Barrel.transform.forward * Force, ForceMode.Impulse);
            Round = null;
            Occupied = false;
        }
    }
}
