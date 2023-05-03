using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    /// <summary>
    /// Animates ping pong opponent robot based on _triggerName string.
    /// </summary>
    class PingPongRoboAnimator: MonoBehaviour
    {
        /// <summary>
        /// Trigger to trigger in animator.
        /// </summary>
        [SerializeField]
        [Tooltip("Trigger to trigger")]
        string _triggerName;

        /// <summary>
        /// Animator component
        /// </summary>
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
