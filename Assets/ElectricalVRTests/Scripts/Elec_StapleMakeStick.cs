using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_StapleMakeStick : MonoBehaviour, IVoltage
{
    public int currentVoltage = 0;
    public int KillAfter = 2;
    public Elec_ToolWireRenderer SpoolItIsON;
    public int ListID;

    private void Awake()
    {
        StartCoroutine(DestroyUnused());
    }
    public void Voltage_Receive(int newVoltage)
    {
        currentVoltage = newVoltage;
        SpoolItIsON.Voltage_Receive(currentVoltage);
    }
    public int Voltage_Send()
    {
        return currentVoltage;
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
