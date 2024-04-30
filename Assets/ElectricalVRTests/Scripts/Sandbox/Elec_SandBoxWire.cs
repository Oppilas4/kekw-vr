using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_SandBoxWire : MonoBehaviour,IVoltage
{
    LineRenderer lineRenderer;
    public GameObject End1, End2;
    public int Voltage = 0;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        lineRenderer.SetPosition(0, End1.transform.position);
        lineRenderer.SetPosition(1, End2.transform.position);
    }

    public void Voltage_Receive(int newVoltage)
    {
        Voltage = newVoltage;
    }

    public int Voltage_Send()
    {
        return Voltage;
    }
}
