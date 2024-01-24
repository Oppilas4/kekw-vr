using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_StapleMakeStick : MonoBehaviour, IVoltage
{

    public Elec_Voltage WireEndVolt;
    public int startVoltage = 5;
    public int KillAfter = 2;
    public Elec_ToolWireRenderer SpoolItIsON;
    public int SpotInList = -1;

    private void Awake()
    {
        WireEndVolt = new Elec_Voltage(startVoltage);
        StartCoroutine(DestroyUnused());
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
    public IEnumerator DestroyUnused()
    {
        yield return new WaitForSeconds(KillAfter);
        if (!GetComponent<XRBaseInteractable>().isSelected)
        {
            Destroy(gameObject);
            SpoolItIsON.WireComponents.RemoveAt(SpotInList);
        }
    }

}
