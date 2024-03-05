using Kekw.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MC_OilBottle : MonoBehaviour
{
    public VisualEffect oilVFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            oilVFX.SendEvent("Pour");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            oilVFX.SendEvent("Stop");
            
        }
    }
}
