using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Gardening
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _SFXSource;

        private static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }
        [SerializeField] private List<AudioObject> _audioObjects = new List<AudioObject>();
        private Dictionary<string, AudioClip> _musicAudios = new Dictionary<string, AudioClip>();
        private Dictionary<string, AudioClip> _SFXAudios = new Dictionary<string, AudioClip>();

        public const string MUSIC_VOLUME_KEY = "musicVolume";
        public const string SFX_VOLUME_KEY = "sfxVolume";


        private void Awake()
        {
            if(_instance != null && _instance!= this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            foreach(var obj in _audioObjects)
            {
                string remake = obj.audioName.ToLower().Trim(); // clear (no whitespaces, no uppercase) version of AudioCLip name
                if (obj.IsMusic)
                {
                    if (_musicAudios.ContainsKey(remake))
                        continue;
                    _musicAudios.Add(remake, obj.audioClip);
                }
                else
                {
                    if (_SFXAudios.ContainsKey(remake))
                        continue;
                    _SFXAudios.Add(remake, obj.audioClip);
                }
            }
        }

        /// <summary>
        /// NOT case or whitespaces sensitive param
        /// </summary>
        /// <param name="audioName"></param>
        public void Play(string audioName)
        {
            string remake = audioName.ToLower().Trim(); // clear (no whitespaces, no uppercase) version of AudioCLip name
            if(_musicAudios.TryGetValue(remake, out AudioClip music))
            {
                _musicSource.clip = music;
                _musicSource.Play();
            }
            else if(_musicAudios.TryGetValue(remake, out AudioClip sfx))
            {
                _SFXSource.clip = sfx;
                _SFXSource.Play();
            }
        }
    }
}
