using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_StapleMakeStick : MonoBehaviour, IVoltage
{

    public Elec_Voltage WireEndVolt;
    public int startVoltage = 5;
    public int KillAfter = 2;
    public Elec_ToolWireRenderer SpoolItIsON;
    public int ListID;

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
        yield return null;
        Vector3[] newPos = new Vector3[SpoolItIsON.WireRenderer.positionCount];
        Debug.Log(ListID);
        yield return new WaitForSeconds(KillAfter); 
        if (!GetComponent<XRBaseInteractable>().isSelected && ListID >= 0)
        {
            SpoolItIsON.WireComponents.RemoveAt(ListID);
            foreach (GameObject Staple in SpoolItIsON.WireComponents)
            {
                Staple.GetComponent<Elec_StapleMakeStick>().ListID--;
            }
            Destroy(gameObject);
        }
    }

}
