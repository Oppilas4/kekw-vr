using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_SandBoxBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {       
        if(other.GetComponent<Elec_SandBoxItem>() != null) 
        {
            other.GetComponent<Elec_SandBoxItem>().RotationToBox();
            other.GetComponent<Elec_SandBoxItem>().PositionToBox(transform.position);
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Elec_SandBoxItem>() != null)
        {
            other.GetComponent<Elec_SandBoxItem>().BackToNormal();
            other.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
