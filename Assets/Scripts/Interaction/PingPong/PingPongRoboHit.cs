﻿using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    /// <summary>
    /// Ping pong game opponent ball hitting.
    /// </summary>
    public class PingPongRoboHit :MonoBehaviour
    {
        /// <summary>
        /// Forward force
        /// </summary>
        [SerializeField]
        [Tooltip("Forward hit force")]
        private float _forceForward = .25f;

        /// <summary>
        /// Force to apply on Y up axis
        /// </summary>
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
