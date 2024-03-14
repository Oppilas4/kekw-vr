using Kekw.VuoksiBotti;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MC_BotTalk : MonoBehaviour
{
    /// <summary>
    /// Bots mover
    /// </summary>
    [SerializeField]
    [Tooltip("Mover component")]
    MC_TutorialBotMover _mover;

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

    [SerializeField]
    [Tooltip("Text object where the text will be shown")]
    TextMeshPro _textMeshProUGUI;

    [SerializeField]
    [Tooltip("Sentances to link to voice lines")]
    private List<string> botTalk = new List<string>();

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

            // Generate a random index
            int randomIndex = UnityEngine.Random.Range(0, _audioClips.Length);

            // Use the random index to select the audio clip and text string
            AudioClip temp = _audioClips[randomIndex];
            string textToShow = botTalk[randomIndex];

            _textMeshProUGUI.text = textToShow;

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
            _mover.SetPause();
        }
        else
        {
            _mover.UnPause();
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
        _textMeshProUGUI.text = null;
    }
}

