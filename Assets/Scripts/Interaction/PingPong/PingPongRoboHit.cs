using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    class PingPongRoboHit:MonoBehaviour
    {
        protected readonly static float PINGPONG_BOT_FORCE = 2f;

        Rigidbody _cache;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PingBall"))
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * PINGPONG_BOT_FORCE, ForceMode.Impulse);
            }
        }
    }
}
