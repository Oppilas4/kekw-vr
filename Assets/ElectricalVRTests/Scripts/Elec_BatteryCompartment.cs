using Oculus.Interaction;
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
    public Transform ejectionPort;
    XRBaseInteractor interactor;
    void Start()
    {
        PapaRemote = gameObject.GetComponentInParent<Elec_TVremote>();
        PapaInteractable = PapaRemote.GetComponent<XRBaseInteractable>();
        PapaInteractable.onSelectEntered.AddListener(GetHand);
        PapaInteractable.onSelectExited.AddListener(HandIsNull);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Battery" && !Occupied)
        {          
            other.gameObject.GetComponent<XRBaseInteractable>().enabled = false;
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
        if (interactor != null)
        {
            if (interactor.transform.gameObject.tag == "LeftHand" && Input.GetButtonDown("XRI_Left_PrimaryButton"))
            {           
                EjectBattery();
            }
            if (interactor.transform.gameObject.tag == "RightHand" && Input.GetButtonDown("XRI_Right_PrimaryButton"))
            {               
                EjectBattery();
            }
        }

    }
    void EjectBattery()
    {
        
        if(battery != null )
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
    void GetHand(XRBaseInteractor SelectingInteractor)
    {
        interactor = SelectingInteractor;
    }
    void HandIsNull(XRBaseInteractor SelectingInteractor)
    {
        interactor = null;
    }
}
