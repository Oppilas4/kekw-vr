using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_Stamper : MonoBehaviour
{
    Rigidbody body;
    public List<GameObject> list;
    public Transform DecalPos;
    public Quaternion DecalRot;
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (body.velocity.magnitude > 0.5)
        {
            Stamp(Quaternion.FromToRotation(Vector3.up, collision.GetContact(0).normal) * DecalRot);
        }
    }
    void Stamp(Quaternion rot)
    {
        Instantiate(list[Random.Range(0, list.Count)],DecalPos.position,rot);
    }
}
