using System;
using UnityEngine;

namespace Kekw.Interaction.Football
{
    /// <summary>
    /// Simulates kick, negates velocity
    /// </summary>
    class Kick: MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("FootBall"))
            {
                GameObject ball = collision.gameObject;
                Rigidbody ballRB = ball.GetComponent<Rigidbody>();
                Vector3 velocity = ballRB.velocity;
                ballRB.AddForce(-velocity, ForceMode.Impulse);
            }
        }
    }
}
