using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_ShakingCondition : MonoBehaviour
{
    public float forceSpeed = 2f;
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody StuffRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        StuffRigidbody.AddForce(transform.right * forceSpeed, ForceMode.Impulse);
    }
}
