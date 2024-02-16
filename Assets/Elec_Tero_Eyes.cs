using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_Tero_Eyes : MonoBehaviour
{
    public GameObject toLookAt;


    private void Start()
    {
        toLookAt = Camera.main.gameObject;
    }

    private void LateUpdate()
    {
        transform.LookAt(toLookAt.transform.position);
    }
}
