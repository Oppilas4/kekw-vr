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
            // Todo call _paddleSpawner and _ballSpawner Spawn() method and save returned objects to _trackedObjects
            // Call xxx.SetMissionManager(this);
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionStop()
        {
            // call tracked object OnDestroyRequested method on all tracked objects.
            // clear list
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

