using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_RamiEye : MonoBehaviour
{
    public GameObject LookAtWhat;
    void Start()
    {
        LookAtWhat = GameObject.Find("Main Camera");
    }
    void Update()
    {
        transform.up = LookAtWhat.transform.position - transform.position;
    }
}
