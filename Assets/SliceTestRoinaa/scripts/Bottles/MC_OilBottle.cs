using Kekw.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MC_OilBottle : MonoBehaviour
{
    public VisualEffect oilVFX;
    public Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            animator.SetTrigger("Open");
            oilVFX.SendEvent("Pour");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pan"))
        {
            animator.SetTrigger("Close");
            oilVFX.SendEvent("Stop");
        }
    }
}
