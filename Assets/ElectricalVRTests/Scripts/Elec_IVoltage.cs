using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Elec_IVoltage
{
    public abstract void Voltage_Receive(int newVoltage);
    public abstract int Voltage_Send();
}
