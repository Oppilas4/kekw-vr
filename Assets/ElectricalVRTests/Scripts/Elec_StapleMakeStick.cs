using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_StapleMakeStick : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 13)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }

    }

}
