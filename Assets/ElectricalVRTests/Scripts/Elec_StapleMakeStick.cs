using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_StapleMakeStick : MonoBehaviour, IVoltage
{

    public Elec_Voltage WireEndVolt;
    public int startVoltage = 5;



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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 13)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }

    }

}
