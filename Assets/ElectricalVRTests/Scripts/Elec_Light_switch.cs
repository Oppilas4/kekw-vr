using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_Light_switch : MonoBehaviour
{
    public GameObject on;
    public GameObject off;
    public bool ison = false;
    private void Start()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
            if (other.tag == "Player")
            {                
                if (!ison)
                {
                    off.SetActive(false);
                    on.SetActive(true);
                }
                else
                {
                    off.SetActive(true);
                    on.SetActive(false);
                }
            }
    }
}
