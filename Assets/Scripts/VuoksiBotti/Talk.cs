using UnityEngine;
using System.Collections;
using Kekw.Interaction;

namespace Kekw.VuoksiBotti
{
    class Talk: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Animation manager component")]
        AnimationManager _animationManager;

        [SerializeField]
        [Tooltip("BoomBox component")]
        BoomBox _boomBox;

        [SerializeField]
        [Tooltip("Mover component")]
        Mover _mover;

        [SerializeField]
        [Tooltip("Source where robots voice will be played.")]
        AudioSource _audioSource;

        [SerializeField]
        [Tooltip("Audio tracks that robot can talk.")]
        AudioClip[] _audioClips;

        bool _isTalking = false;

        /// <summary>
        /// Speak single voice line. Hook to ui button "Talk".
        /// </summary>
        public void TalkSingle()
        {
            if (!_audioSource.isPlaying && !_isTalking)
            {
                _isTalking = true;
                NotifyAnimatorOfSpeech(true);
                NotifyBoomBoxOfSpeech(true);
                NotifyMoverOfSpeech(true);
                _audioSource.PlayOneShot(_audioClips[UnityEngine.Random.Range(0, _audioClips.Length)]);
                StartCoroutine(WaitForSpeechEnd(_audioSource.clip.length + .5f));
            }
        }

        /// <summary>
        /// Set animator to speech mode.
        /// </summary>
        private void NotifyAnimatorOfSpeech(bool isTalking)
        {
            if (isTalking)
            {
                _animationManager.IsTalking = true;
            }
            else
            {
                _animationManager.IsTalking = false;
            }
        }

        /// <summary>
        /// Set mover component to pause mode.
        /// </summary>
        private void NotifyMoverOfSpeech(bool isTalking)
        {
            if (isTalking)
            {
                _mover.SetPause();
            }
            else
            {
                _mover.UnPause();
            }
        }

        /// <summary>
        /// Lower boom box volume while talking.
        /// </summary>
        private void NotifyBoomBoxOfSpeech(bool isTalking)
        {
            if (isTalking)
            {
                _boomBox.SetToSpeechMode();
            }
            else
            {
                _boomBox.ReleaseFromSpeechMode();
            }
        }


        IEnumerator WaitForSpeechEnd(float length)
        {
            yield return new WaitForSeconds(length);
            NotifyAnimatorOfSpeech(false);
            NotifyBoomBoxOfSpeech(false);
            NotifyMoverOfSpeech(false);
            _isTalking = false;
        }
    }
}
