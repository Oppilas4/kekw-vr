using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_Light_switch : MonoBehaviour
{
    public Animator animator;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.tag == "Player" && animator.GetBool("on/off") == false )
        {
            animator.SetBool("on/off", true);
        }
        if (other.tag == "Player" && animator.GetBool("on/off") == true)
        {
            animator.SetBool("on/off", false);
        }
        Debug.Log("Triggered");
    }
}
