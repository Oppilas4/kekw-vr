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
                // Test stopping ball at robot and "hitting" ----->
                Rigidbody ballBody = collision.gameObject.GetComponent<Rigidbody>();
                ballBody.isKinematic = true;
                ballBody.velocity = Vector3.zero;
                ballBody.isKinematic = false;
                // <----- Test stopping ball at robot and "hitting"
                ballBody.AddForce(Vector3.forward * PINGPONG_BOT_FORCE, ForceMode.Impulse);
            }
        }
    }
}
