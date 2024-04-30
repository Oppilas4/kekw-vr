using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_Stamper : MonoBehaviour
{
    Rigidbody body;
    public List<GameObject> list;
    public Transform DecalPos;
    XRBaseInteractable interactable;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        interactable = GetComponent<XRBaseInteractable>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (body.velocity.magnitude > 0.25 && interactable.isSelected)
        {
            Stamp();
        }
    }
    void Stamp()
    {
        Instantiate(list[Random.Range(0, list.Count)],DecalPos.position,DecalPos.rotation);
    }
}
