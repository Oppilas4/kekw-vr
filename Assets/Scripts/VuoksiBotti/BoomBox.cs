using UnityEngine;
using Kekw.Common;
using System.Collections.Generic;

namespace Kekw.VuoksiBotti
{
    /// <summary>
    /// Robots boombox audio player.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    class BoomBox : MonoBehaviour, IPause
    {
        AudioSource _audioSource;

        [SerializeField]
        [Tooltip("Audio playlist")]
        AudioClip[] _audioClips;

        Queue<AudioClip> _clipQueue;

        AudioClip _currentPlaying;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            // Build audio playlist queue from assigned clips
            _clipQueue = new Queue<AudioClip>(_audioClips);
        }

        private void Start()
        {
            QueueNextClip();
        }

        private void Update()
        {
            if (_audioSource.isPlaying) return;
            QueueNextClip();
        }

        private void QueueNextClip()
        {
            _currentPlaying = _clipQueue.Dequeue();
            _audioSource.clip = _currentPlaying;
            _audioSource.Play();
            _clipQueue.Enqueue(_currentPlaying);
        }

        /// <summary>
        /// <seealso cref="IPause"/>
        /// </summary>
        public void SetPause()
        {
            if(_audioSource.isPlaying)
            {
                _audioSource.volume = 0f;
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
            if (!_audioSource.isPlaying)
            {
                _audioSource.volume = 1f;
            }
        }

        /// <summary>
        /// Lovers volume of the boom box audio
        /// </summary>
        public void SetToSpeechMode()
        {
            _audioSource.volume = .25f;
        }

        /// <summary>
        /// Lovers volume of the boom box audio
        /// </summary>
        public void ReleaseFromSpeechMode()
        {
            _audioSource.volume = 1f;
        }
    }
}
