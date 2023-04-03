using UnityEngine;
using Kekw.Mission;

namespace Kekw.Interaction
{
    /// <summary>
    /// Correct knob trigger behaviour.
    /// </summary>
    class KnobCorrect: MonoBehaviour
    {
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
