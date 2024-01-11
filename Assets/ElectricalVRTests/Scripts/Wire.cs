using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour, IVoltage
{
    LineRenderer wire;
    public GameObject start, end;
    public Elec_Voltage WireVoltage;
    public int startVoltage = 0;

    private void Awake()
    {
        WireVoltage = new Elec_Voltage(startVoltage);
    }
    void Start()
    {
        wire = GetComponent<LineRenderer>();
        wire.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        wire.SetPosition(0, start.transform.position);
        wire.SetPosition(1, end.transform.position);
    }

    public void Voltage_Receive(int newVoltage)
    {
        WireVoltage.voltage = newVoltage;
    }

    public int Voltage_Send()
    {
        return WireVoltage.voltage;
    }
}
