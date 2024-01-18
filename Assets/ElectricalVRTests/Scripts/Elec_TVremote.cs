using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_TVremote : MonoBehaviour
{
    public Elec_Televisio televisio;
    public int Batteries = 0;
    XRBaseInteractable interactable;
    public GameObject spawnpoint;
    public float throwForce = 10;  
    private void Start()
    {
      interactable = GetComponent<XRBaseInteractable>();
    }
    
    public void ButtonPress()
    {
        if (Batteries == 2 && televisio != null)
        {
            televisio.SwitchChannel();
        }

    }
    public void SecretToyGunMethod(GameObject projectile)
    {
        if (Batteries == 1)
        {
            GameObject bulletz = Instantiate(projectile, spawnpoint.transform.position + spawnpoint.transform.forward, gameObject.transform.rotation);
            Rigidbody rb = bulletz.GetComponent<Rigidbody>();
            if (rb != null)

            {
                rb.AddForce(spawnpoint.transform.forward * throwForce, ForceMode.VelocityChange);
            }
        }

    }
}
