using System;
using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    class PingPongRoboAnimator: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Trigger to trigger")]
        string _triggerName;

        [SerializeField]
        [Tooltip("Robo animator")]
        Animator _roboAnimator;


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PingBall"))
            {
                _roboAnimator.SetTrigger(_triggerName);
            }
        }

        /// <summary>
        /// Set robo to game mode or off game mode
        /// </summary>
        /// <param name="onOff"></param>
        public void SetGameMode(bool onOff)
        {
            _roboAnimator.SetBool("Game", onOff);
        }
    }
}
