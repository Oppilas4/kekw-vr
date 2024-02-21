using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (!collision.CompareTag("comb")) return;
        Vector3 direction = ( transform.position -collision.transform.position).normalized;
        rb.AddForce(direction * 10f);
    }
}
