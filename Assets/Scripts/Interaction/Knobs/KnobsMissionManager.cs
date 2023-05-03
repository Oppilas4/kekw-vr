using Kekw.Manager;
using Kekw.Mission;
using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Make easter egg scene mission manager.
    /// </summary>
    public class KnobsMissionManager :MonoBehaviour,  IMissionManager
    {
        /// <summary>
        /// Num of knobs
        /// </summary>
        [SerializeField]
        [Tooltip("How many knobs needs to be correct.")]
        int _correctLimit;

        /// <summary>
        /// Audio played when mission fails.
        /// </summary>
        [SerializeField]
        [Tooltip("Audio source playing clips")]
        AudioSource _audioSourceFail;

        /// <summary>
        /// Audio played when mission succeeds
        /// </summary>
        [SerializeField]
        [Tooltip("Audio source playing when teleporting clips")]
        AudioSource _audioSourceSuccess;


        int _correctKnobs = 0;

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
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

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionStart()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionStop()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
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
