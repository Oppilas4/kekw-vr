using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_SandBoxBody : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {       
        if(other.GetComponent<Elec_SandBoxItem>() != null) 
        {
            other.GetComponent<Elec_SandBoxItem>().PositionToBox(transform.position);
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
