using UnityEngine;
using Kekw.Mission;
using System.Collections.Generic;

namespace Kekw.Interaction.PingPong
{
    /// <summary>
    /// Mannages ping pong gameplay
    /// </summary>
    public class PingPongManager : MonoBehaviour, IMissionManager, IIngameButtonLogic
    {
        [SerializeField]
        [Tooltip("Paddle spawn point")]
        PingPongSpawner _paddleSpawn;

        [SerializeField]
        [Tooltip("Ball spawn point")]
        PingPongSpawner _ballSpawn;
        
        [SerializeField]
        [Tooltip("Robo animator")]
        PingPongRoboAnimator _pingPongRoboAnimator;

        List<GameObject> _trackedObjects;

        //initilaize tracked game equipment list
        private void Awake() => _trackedObjects = new List<GameObject>();

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionStart()
        {

            if(_trackedObjects.Count==0)
            {
                _trackedObjects.Add(_paddleSpawn.Spawn(this));
                _trackedObjects.Add(_ballSpawn.Spawn(this));
                _pingPongRoboAnimator.SetGameMode(true);
            }
            else
            {
                OnMissionStop();
                OnMissionStart();
            }

        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionStop()
        {
            for(int i = 0; i<_trackedObjects.Count; i++)
            {
                _trackedObjects[i].GetComponent<PingPongItem>().OnDestroyRequested();
            }
            _trackedObjects.Clear();
            _pingPongRoboAnimator.SetGameMode(false);
        }

        /// <summary>
        /// Never gonna win so cannot lose, pepehands...
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionFail() => throw new System.Exception("Method is not needed in ping pong game");

        /// <summary>
        /// Never gonna win, pepehands...
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionSuccess() => throw new System.Exception("Method is not needed in ping pong game");

        public void TriggerAction() => OnMissionStart();
    } 
}

