using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    class PingPongRoboHit:MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Forward hit force")]
        private float _forceForward = .25f;

        [SerializeField]
        [Tooltip("Upward hit force")]
        private float _forceUp = .25f;


        Vector3 _hitDirection;

        private void Awake()
        {
            _hitDirection = transform.forward;
            _hitDirection *= _forceForward;
            _hitDirection.y = 1f * _forceUp;
        }

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
#if UNITY_EDITOR
                _hitDirection = transform.forward;
                _hitDirection *= _forceForward;
                _hitDirection.y = 1f * _forceUp;
                
#endif
                ballBody.AddForce(_hitDirection, ForceMode.Impulse);
            }
        }
    }
}
