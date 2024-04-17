using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_SandBoxWireEnd : MonoBehaviour, IVoltage
{
    public Elec_SandBoxWire mamaWire;

    public void Voltage_Receive(int newVoltage)
    {
        mamaWire.Voltage = newVoltage;
    }

    public int Voltage_Send()
    {
        return mamaWire.Voltage;
    }    
}
