using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_BatteryBox : MonoBehaviour
{
    GameObject battery;
    bool Occupied = false;
    XRBaseInteractable ThisInteractable;
    public List<Transform> BatteryPositions = new List<Transform>();
    int BatteriesIn = 0;
    public int Voltage = 5; 
    // Start is called before the first frame update
    void Start()
    {
        ThisInteractable = GetComponent<XRBaseInteractable>();
        ThisInteractable.onSelectEntered.AddListener(SetVoltage);
        ThisInteractable.onSelectExited.AddListener(DeleteVoltage);
    }
    
    private void SetVoltage(XRBaseInteractor arg0)
    {
        arg0.GetComponent<Elec_SandNode>().currentVoltage = Voltage;
    }
    void DeleteVoltage(XRBaseInteractor arg0) 
    {
        arg0.GetComponent<Elec_SandNode>().currentVoltage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Battery" && !Occupied)
        {
            other.gameObject.GetComponent<XRBaseInteractable>().enabled = false;
            battery = other.gameObject;
            other.enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.parent = BatteryPositions[BatteriesIn].transform;
            other.gameObject.transform.position = BatteryPositions[BatteriesIn].transform.position;
            other.gameObject.transform.rotation = BatteryPositions[BatteriesIn].transform.rotation;
            Occupied = true;
            BatteriesIn++;
        }
    }
    
}
