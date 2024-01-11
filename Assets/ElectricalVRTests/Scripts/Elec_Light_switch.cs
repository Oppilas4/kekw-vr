using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_Light_switch : MonoBehaviour
{
    public Animator animator;
    public bool is_on = false;
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && is_on == false)
        {
            animator.SetTrigger("On");
            is_on = true;
        }
        if (other.tag == "Player" && is_on != false)
        {
            animator.SetTrigger("Off");
            is_on = false;
        }
    }
}
