using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_LostNFound : MonoBehaviour
{
    public Transform Box, PlayerSpawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<XRBaseInteractable>() != null)
        {
            if(other.tag != "Player" && !other.GetComponent<XRGrabInteractable>().isSelected)other.gameObject.transform.position = Box.position;
            else other.transform.position = PlayerSpawn.position;
        }
        

    }
}
