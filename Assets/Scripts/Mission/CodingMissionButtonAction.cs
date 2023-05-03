using UnityEngine;
using Kekw.Interaction;

namespace Kekw.Mission
{
    /// <summary>
    /// Coding mission activation button scripts.
    /// </summary>
    public class CodingMissionButtonAction : MonoBehaviour, IIngameButtonLogic
    {
        /// <summary>
        /// Will this button start mission?
        /// </summary>
        [SerializeField]
        [Tooltip("Is this the mission start button?")]
        bool _missionStartButton;

        /// <summary>
        /// Coding mission manager
        /// </summary>
        [SerializeField]
        [Tooltip("Actual mission manager")]
        CodingMissionManager _codingMission;
        
        /// <summary>
        /// <seealso cref="IIngameButtonLogic"/>
        /// </summary>
        public void TriggerAction()
        {
            if (_missionStartButton)
            {
                _codingMission.OnMissionStart();
            }

            if (!_missionStartButton)
            {
                _codingMission.OnMissionStop();
            }
        }
    }
}
