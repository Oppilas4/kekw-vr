using UnityEngine;
using Kekw.Common;
using UnityEngine.Playables;

namespace Kekw.VuoksiBotti
{
    /// <summary>
    /// Robots boombox audio player.
    /// </summary>
    [RequireComponent(typeof(PlayableDirector))]
    class BoomBox : MonoBehaviour, IPause
    {
        PlayableDirector _playableDirector;

        private void Awake()
        {
            _playableDirector = GetComponent<PlayableDirector>();
        }

        /// <summary>
        /// <seealso cref="IPause"/>
        /// </summary>
        public void SetPause()
        {
            if(_playableDirector.state == PlayState.Playing)
            {
                _playableDirector.Pause();
            }
            else
            {
                UnPause();
            }
        }

        /// <summary>
        /// <seealso cref="IPause"/>
        /// </summary>
        public void UnPause()
        {
            if (_playableDirector.state == PlayState.Paused)
            {
                _playableDirector.Play();
            }
        }
    }
}
