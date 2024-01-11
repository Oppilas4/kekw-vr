using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_Light_switch : MonoBehaviour
{
    public Animator animator;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (!animator.GetBool("TurnedOn"))
        {
            animator.SetBool("TurnedOn", true);
        }
        if (animator.GetBool("TurnedOn"))
        {
            animator.SetBool("TurnedOn", false);
        }
        Debug.Log("Triggered");
    }
}
