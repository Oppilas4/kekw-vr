using System;
using Kekw.Manager;
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
        /// <seealso cref="IDestroyable"/>
        /// </summary>
        public void OnDestroyRequested()
        {
            // Todo destroy possible vfx effects
            Destroy(this.gameObject);
        }

        public void SetMissionManager(PingPongManager missionManager) => _missionManager = missionManager;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("KillPlane"))
            {
               _missionManager.OnMissionStop();
            }
        }
    }
}
