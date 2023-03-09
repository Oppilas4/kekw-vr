using UnityEngine;
using UnityEngine.Timeline;
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

        }

        public void SetPause()
        {
            
        }

        public void UnPause()
        {
            throw new System.NotImplementedException();
        }
    }
}
