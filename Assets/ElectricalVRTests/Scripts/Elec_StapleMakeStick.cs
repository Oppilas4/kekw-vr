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
        if (collision.gameObject.layer != 13  && collision.gameObject.layer != 19 )
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            StartCoroutine(DestroyUnused());
        }
    }
    public IEnumerator DestroyUnused()
    {
        yield return null;
        KillAfter = KillAfter + SpoolItIsON.WireComponents.Count / 10;
        yield return new WaitForSeconds(KillAfter);
        if (gameObject != null) DestroySafely();
    }
    void Update() 
    {
        ListID = SpoolItIsON.WireComponents.IndexOf(gameObject);
    }
    public void DestroySafely()
    {
        if (!GetComponent<XRBaseInteractable>().isSelected && ListID >= 0)
        {
            SpoolItIsON.WireComponents.RemoveAt(ListID);
            Destroy(gameObject);
        }
    }
}
