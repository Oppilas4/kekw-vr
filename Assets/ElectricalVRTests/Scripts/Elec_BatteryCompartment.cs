using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_BatteryCompartment : MonoBehaviour
{
    public Elec_TVremote PapaRemote;
    bool Occupied = false;
    GameObject battery;
    XRBaseInteractable PapaInteractable;
    bool selected = false;
    public Transform ejectionPort;
    void Start()
    {
        PapaRemote = gameObject.GetComponentInParent<Elec_TVremote>();
        PapaInteractable = PapaRemote.GetComponent<XRBaseInteractable>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<XRBaseInteractable>() != null && !Occupied)
        {
            other.gameObject.GetComponent<XRBaseInteractable>().enabled = false;
        }
        if (other.tag == "Battery" && !Occupied)
        {
            battery = other.gameObject;
            other.enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.parent = gameObject.transform;
            other.gameObject.transform.position = gameObject.transform.position;
            other.gameObject.transform.rotation = gameObject.transform.rotation;
            PapaRemote.Batteries++;
            Occupied = true;
        }
    }
    private void Update()
    {
        selected = PapaInteractable.isSelected;
        if(selected && Input.GetButtonDown("XRI_Right_PrimaryButton") && battery != null )
        {
            battery.transform.parent = null;
            battery.GetComponent<Collider>().enabled = true;
            battery.GetComponent<Rigidbody>().isKinematic = false;  
            battery.GetComponent<Rigidbody>().AddForce(ejectionPort.transform.right * 3,ForceMode.Impulse);
            battery.tag = "Untagged";
            battery = null;
            Occupied = false;
            PapaRemote.Batteries--;
        }
    }
}
