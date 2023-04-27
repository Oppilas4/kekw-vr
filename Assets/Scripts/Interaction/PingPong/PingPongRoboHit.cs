using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    class PingPongRoboHit:MonoBehaviour
    {
        public float PINGPONG_BOT_FORCE = .35f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PingBall"))
            { 
                // Stopping ball at robot ----->
                Rigidbody ballBody = collision.gameObject.GetComponent<Rigidbody>();
                ballBody.isKinematic = true;
                ballBody.velocity = Vector3.zero;
                ballBody.isKinematic = false;
                // <----- Stopping ball at robot
                // "Hitting"
                ballBody.AddForce(Vector3.forward * PINGPONG_BOT_FORCE, ForceMode.Impulse);
            }
        }
    }
}
