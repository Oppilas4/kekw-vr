using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MC_SoapBottle : MonoBehaviour
{
    public VisualEffect oilVFX;
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SinkWater") && other.GetComponent<MC_SinkWater>().hasWater())
        {
            animator.SetTrigger("Open");
            oilVFX.SendEvent("Pour");
            other.GetComponent<MC_SinkWater>().EnableSoapEffect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SinkWater"))
        {
            animator.SetTrigger("Close");
            oilVFX.SendEvent("Stop");
        }
    }
}
