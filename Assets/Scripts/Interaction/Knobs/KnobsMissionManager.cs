using Kekw.Manager;
using Kekw.Mission;
using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Knobs easter egg mission
    /// </summary>
    class KnobsMissionManager :MonoBehaviour,  IMissionManager
    {
        [SerializeField]
        [Tooltip("How many knobs needs to be correct.")]
        int _correctLimit;

        [SerializeField]
        [Tooltip("Audio source playing clips")]
        AudioSource _audioSourceFail;

        [SerializeField]
        [Tooltip("Audio source playing when teleporting clips")]
        AudioSource _audioSourceSuccess;


        int _correctKnobs = 0;

        public void OnMissionFail()
        {
            
            if(_correctLimit - 1 >= 0)
            {
                _correctKnobs--;

                if(_correctKnobs == 0)
                {
                    _audioSourceFail.PlayOneShot(_audioSourceFail.clip);
                }
            }
        }

        public void OnMissionStart()
        {
            throw new System.NotImplementedException();
        }

        public void OnMissionStop()
        {
            throw new System.NotImplementedException();
        }

        public void OnMissionSuccess()
        {
            _correctKnobs++;
            if(_correctKnobs >= _correctLimit)
            {
                _audioSourceSuccess.PlayOneShot(_audioSourceSuccess.clip);
                SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
                sceneSwitcher.SetSceneJumpPosition(new Vector3(3.6f, .35f, 2.01f));
                sceneSwitcher.SwitchScene("MakeEasterEgg", false);
            }
        }
    }
}
