using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_ShakingCondition : MonoBehaviour
{
    public float forceSpeed = 2f;

    public Animator dogAnimator;             // Reference to the dog's Animator

    public string shakeAnimationTrigger = "Shake"; // The trigger to start the shake animation
    public bool isShaking = false;
    public void Start()
    {
        if (dogAnimator == null)
        {
            dogAnimator = GetComponent<Animator>(); // Assign the Animator if not set in the inspector
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Cereal")
        {
            StartCoroutine(WaitAndStop());
            Rigidbody StuffRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            StuffRigidbody.AddForce(transform.right * forceSpeed, ForceMode.Impulse);
        }
    }
    private IEnumerator WaitAndStop()
    {
        isShaking = true;
        dogAnimator.SetFloat("Speed", 0);
        dogAnimator.SetTrigger(shakeAnimationTrigger);
        yield return new WaitForSeconds(2.1f);
        isShaking = false;
    }
}
