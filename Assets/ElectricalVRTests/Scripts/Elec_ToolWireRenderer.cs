using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_ToolWireRenderer : MonoBehaviour , IVoltage
{
    public List<GameObject> WireComponents = new List<GameObject>();
    public LineRenderer WireRenderer;
    public Color ColorOfTheWire;
    public Material Lego;
    public Elec_MegaTool ThisStapler;
    int voltage;
    public bool ReadyToBEDestroyed = false;

    public void Voltage_Receive(int newVoltage)
    {
       voltage = newVoltage;
    }

    public int Voltage_Send()
    {
        return voltage;
    }

    void Start()
    {       
       WireRenderer = GetComponent<LineRenderer>();
       WireRenderer.material = Lego;
       WireRenderer.startColor = ColorOfTheWire;
       WireRenderer.endColor = ColorOfTheWire;
       
    }

    void Update()
    {
        WireRenderer.positionCount = WireComponents.Count;
        for (int i = 0; i < WireComponents.Count; i++)
        {
            if (WireComponents[i] != null) { WireRenderer.SetPosition(i, WireComponents[i].transform.position); }          
        }
    }
    public void DisableWireSafely()
    {
        ThisStapler.SwitchWire();
        ThisStapler.WireSpools.Remove(this);
    }
}
