using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elec_Light_switch : MonoBehaviour
{
    public GameObject offObject;
    bool IsOn = false;
    public UnityEvent WharaDo;
    private void Start()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
            if (other.tag == "LeftHand" || other.tag == "RightHand")
            {
                Debug.Log("Player tagged object has entered");
                if (!IsOn)
                {
                    gameObject.SetActive(false);
                    offObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(true);
                    offObject.SetActive(false);
                }
            }
    }
}
