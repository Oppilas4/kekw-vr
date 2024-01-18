using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_TVremote : MonoBehaviour
{
    public Elec_Televisio televisio;
    public int Batteries = 0;
    XRBaseInteractable interactable;
    private void Start()
    {
      interactable = GetComponent<XRBaseInteractable>();

    }
    public void ButtonPress()
    {
        if (Batteries == 2)
        {
            televisio.SwitchChannel();
        }
       
    }
}
