using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    class PingPongRoboHit:MonoBehaviour
    {
        public float PINGPONG_BOT_FORCE = .3f;

        Rigidbody _cache;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PingBall"))
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * PINGPONG_BOT_FORCE, ForceMode.VelocityChange);
            }
        }
    }
}
