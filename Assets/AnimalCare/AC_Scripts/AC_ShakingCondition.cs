using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_ShakingCondition : MonoBehaviour
{
    public float forceSpeed = 2f;

    public Animator dogAnimator;             // Reference to the dog's Animator

    public string shakeAnimationTrigger = "Shake"; // The trigger to start the shake animation

    public void Start()
    {
        if (dogAnimator == null)
        {
            dogAnimator = GetComponent<Animator>(); // Assign the Animator if not set in the inspector
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        Rigidbody StuffRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        StuffRigidbody.AddForce(transform.right * forceSpeed, ForceMode.Impulse);
        dogAnimator.SetTrigger(shakeAnimationTrigger);
    }
}
