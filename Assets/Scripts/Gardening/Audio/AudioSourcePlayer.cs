using UnityEngine;

namespace Gardening
{
    public class AudioSourcePlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private string _audioClipName;

        private AudioClip _audioClip;

        private void Start()
        {
            _audioClip = AudioManager.Instance.GetAudio(_audioClipName);
            _source.clip = _audioClip;
        }

        public void PlayClip()
        {
            _source.Play();
        }

        /// <summary>
        /// Can be useful in case of looped sounds
        /// </summary>
        public void PauseClip()
        {
            _source.Pause();
        }
    }
}
