
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_BatteryBox : MonoBehaviour, IVoltage 
{
    GameObject battery;
    XRBaseInteractable ThisInteractable;
    public List<Transform> BatteryPositions = new List<Transform>();
    int BatteriesIn = 0;
    public int Voltage = 5;

    [System.Obsolete]
    void Start()
    {
        ThisInteractable = GetComponent<XRBaseInteractable>();
        ThisInteractable.onSelectExited.AddListener(DeleteVoltage);
    }
    void DeleteVoltage(XRBaseInteractor arg0) 
    {
       
    }
    void Update()
    {
        if (BatteriesIn == 2)
        {
            GetComponent<Elec_SandBoxItem>().Voltage = Voltage;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery" && BatteriesIn < 2)
        {
            other.gameObject.GetComponent<XRBaseInteractable>().enabled = false;
            battery = other.gameObject;
            other.enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.parent = BatteryPositions[BatteriesIn].transform;
            other.gameObject.transform.position = BatteryPositions[BatteriesIn].transform.position;
            other.gameObject.transform.rotation = BatteryPositions[BatteriesIn].transform.rotation;
            BatteriesIn++;
        }
    }

    public void Voltage_Receive(int newVoltage)
    {
        
    }

    public int Voltage_Send()
    {
        if (BatteriesIn == 2) return Voltage;
        else return 0;
    }
}
