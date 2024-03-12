using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HS_AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private static HS_AudioManager _instance;
    public static HS_AudioManager Instance { get { return _instance; } }
    [SerializeField] private List<HS_AudioObject> _audioObjects = new List<HS_AudioObject>();
    private Dictionary<string, AudioClip> _audios = new Dictionary<string, AudioClip>();

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
            if (_audios.ContainsKey(remake))
                continue;
            _audios.Add(remake, obj.audioClip);
        }
    }

    /// <summary>
    /// NOT case or whitespaces sensitive param
    /// </summary>
    /// <param name="audioName"></param>
    public AudioClip GetAudio(string audioName)
    {
        string remake = audioName.ToLower().Trim(); // clear (no whitespaces, no uppercase) version of AudioCLip name
        if (_audios.TryGetValue(remake, out AudioClip clip))
        {
            return clip;
        }
        else
            return null;
    }
}
