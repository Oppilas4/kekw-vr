using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_Light_switch : MonoBehaviour
{
    public bool is_on = false;
    public GameObject on;
    public GameObject off;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.tag == "Player" && is_on == false)
        {
            off.SetActive(false);
            on.SetActive(true);
            is_on = true;
            Debug.Log("Lights on");
        }
        if (other.tag == "Player" && is_on == true)
        {
            on.SetActive(false);
            off.SetActive(true);
            is_on = false;
        }
    }
}
