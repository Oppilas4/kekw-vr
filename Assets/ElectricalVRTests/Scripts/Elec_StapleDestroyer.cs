using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_StapleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.GetComponent<Elec_StapleMakeStick>() != null)
        {
            other.gameObject.GetComponent<Elec_StapleMakeStick>().DestroySafely();
        }

    }
}
