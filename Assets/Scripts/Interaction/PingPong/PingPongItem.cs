using UnityEngine;

namespace Kekw.Interaction.PingPong
{
    /// <summary>
    /// Item inside ping pong game. Like baddle and ball.
    /// </summary>
    class PingPongItem : MonoBehaviour, IDestroyable
    {
        PingPongManager _missionManager;

        /// <summary>
        /// Audio to play when collision enters. OPTIONAL.
        /// </summary>
        [SerializeField]
        [Tooltip("Play hit audio if assigned ")]
        AudioSource _BallHitAudio;

        /// <summary>
        /// <seealso cref="IDestroyable"/>
        /// </summary>
        public void OnDestroyRequested()
        {
            // Todo destroy possible vfx effects
            Destroy(this.gameObject);
        }

        /// <summary>
        /// Set mission manager on spawn.
        /// </summary>
        /// <param name="missionManager">PingPongManager that controls game cycles.</param>
        public void SetMissionManager(PingPongManager missionManager) => _missionManager = missionManager;

        private void OnCollisionEnter(Collision collision)
        {
            if (_BallHitAudio != null)
            {
                _BallHitAudio.PlayOneShot(_BallHitAudio.clip);
            }

            if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("KillPlane"))
            {
               _missionManager.OnMissionStop();
            }
            
        }
    }
}
