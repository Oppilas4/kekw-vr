using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_SandBoxBody : MonoBehaviour
{
    // Start is called before the first frame update
    public Quaternion RotationOnEnter;
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
            other.transform.rotation = RotationOnEnter ;
            other.transform.position = new Vector3(transform.position.x,other.transform.position.y,other.transform.position.z);
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Rigidbody>().isKinematic = false;
    }
}
