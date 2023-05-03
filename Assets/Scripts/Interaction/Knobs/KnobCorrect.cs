using UnityEngine;
using Kekw.Mission;

namespace Kekw.Interaction
{
    /// <summary>
    /// Knobs hit this collider and when knob is detected sends signal to <seealso cref="IMissionManager"/>.
    /// </summary>
    class KnobCorrect: MonoBehaviour
    {
        /// <summary>
        /// Gameobject that contains mission manager relative to this mission.<br></br>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        [SerializeField]
        [Tooltip("Knob mission manager")]
        GameObject _missionManager;

        IMissionManager _manager;

        private void Start()
        {
            _manager = _missionManager.GetComponent<IMissionManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("KnobTrigger"))
            {
                _manager.OnMissionSuccess();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("KnobTrigger"))
            {
                _manager.OnMissionFail();
            }
        }
    }
}
