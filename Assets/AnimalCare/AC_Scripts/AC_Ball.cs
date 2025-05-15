using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Ball : MonoBehaviour
{
    public bool isOnGround = false;
    private void Update()
    {
        if (transform.position.y > 0.05)
        {
            isOnGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
