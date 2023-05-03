using UnityEngine;
using System.Collections;
using Kekw.Common;

namespace Kekw.VuoksiBotti
{
    /// <summary>
    /// Vuoksi GBT robots talk chip.
    /// </summary>
    public class Talk : MonoBehaviour, IPause
    {
        /// <summary>
        /// Bots animation manager
        /// </summary>
        [SerializeField]
        [Tooltip("Animation manager component")]
        AnimationManager _animationManager;

        /// <summary>
        /// Bots boombox
        /// </summary>
        [SerializeField]
        [Tooltip("BoomBox component")]
        BoomBox _boomBox;

        /// <summary>
        /// Bots mover
        /// </summary>
        [SerializeField]
        [Tooltip("Mover component")]
        Mover _mover;

        /// <summary>
        /// Talking audio source
        /// </summary>
        [SerializeField]
        [Tooltip("Source where robots voice will be played.")]
        AudioSource _audioSource;

        /// <summary>
        /// Robots talking clips
        /// </summary>
        [SerializeField]
        [Tooltip("Audio tracks that robot can talk.")]
        AudioClip[] _audioClips;

        bool _isTalking = false;

        bool _isPaused = false;

        public void SetPause()
        {
            if (!_isPaused)
            {
                _isPaused = true;
            }
            else
            {
                UnPause();
            }
        }

        public void UnPause()
        {
            if (_isPaused)
            {
                _isPaused = false;
            }
        }


        /// <summary>
        /// Speak single voice line. Hook to ui button "Talk".
        /// </summary>
        public void TalkSingle()
        {
            if (!_audioSource.isPlaying && !_isTalking && !_isPaused)
            {
                _isTalking = true;
                NotifyOtherComponents(true);
                AudioClip temp = _audioClips[UnityEngine.Random.Range(0, _audioClips.Length)];
                _audioSource.PlayOneShot(temp);
                StartCoroutine(WaitForSpeechEnd(temp.length + .5f));
            }
        }

        /// <summary>
        /// NOtify other components that bot should speak.
        /// </summary>
        /// <param name="isTalking"></param>
        private void NotifyOtherComponents(bool isTalking)
        {
            if (isTalking)
            {
                _animationManager.IsTalking = true;
                _mover.SetPause();
                _boomBox.SetPause();
            }
            else
            {
                _animationManager.IsTalking = false;
                _mover.UnPause();
                _boomBox.UnPause();
            }
        }

        /// <summary>
        /// Wait for audio clip to be sinished playing then notify other components.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        IEnumerator WaitForSpeechEnd(float length)
        {
            yield return new WaitForSeconds(length);
            NotifyOtherComponents(false);
            _isTalking = false;
        }
    }
}
