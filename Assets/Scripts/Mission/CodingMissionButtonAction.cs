using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kekw.Interaction;

namespace Kekw.Mission
{
    /// <summary>
    /// Coding mission button actions.
    /// </summary>
    public class CodingMissionButtonAction : MonoBehaviour, IIngameButtonLogic
    {

        [SerializeField]
        [Tooltip("Is this the mission start button?")]
        bool _missionStartButton;

        [SerializeField]
        [Tooltip("Actual mission manager")]
        CodingMissionManager codingMission;
        
        /// <summary>
        /// <seealso cref="IIngameButtonLogic"/>
        /// </summary>
        public void TriggerAction()
        {
            if (_missionStartButton)
            {
                codingMission.OnMissionStart();
            }

            if (!_missionStartButton)
            {
                codingMission.OnMissionStop();
            }
        }
    }
}
