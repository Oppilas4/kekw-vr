using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_WireEnds : MonoBehaviour, IVoltage
{
    public Elec_Voltage WireEndVolt;
    public int startVoltage = 5;


    public Wire MainestWire;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {    
        WireEndVolt = MainestWire.WireVoltage;
    }
    private void Awake()
    {
        WireEndVolt = new Elec_Voltage(startVoltage);
    }

    public void Voltage_Receive(int newVoltage)
    {
        WireEndVolt.voltage = newVoltage;
    }

    public int Voltage_Send()
    {
        return WireEndVolt.voltage;
    }
}
