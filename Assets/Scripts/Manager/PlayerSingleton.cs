using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

namespace Kekw.Manager
{
    /// <summary>
    /// Simple singleton that survives scene changes.
    /// </summary>
    class PlayerSingleton: MonoBehaviour
    {

        [SerializeField]
        [Tooltip("XR origin teleportation provider")]
        TeleportationProvider _teleportationProvider;

        [SerializeField]
        [Tooltip("Xr interaction manager")]
        XRInteractionManager _xRInteractionManager;

        public static GameObject Instance { get => _playerInstance; }
        private static GameObject _playerInstance;

        private void Awake()
        {
            if (_playerInstance && _playerInstance != this.gameObject)
            {
                Destroy(this.gameObject);
            }
            else if(_playerInstance == null)
            {
                _playerInstance = this.gameObject;
                DontDestroyOnLoad(this.gameObject);
                SetupSceneXR();
            }
        }

        /// <summary>
        /// Sets up scene XR interactables, teleport areas, interaction managers etc.
        /// Call this on scene changes.
        /// </summary>
        public void SetupSceneXR()
        {
            SetTeleportationAreas();
            SetInteractables();
            CleanUpUselessInteractionManagers();
        }

        /// <summary>
        /// Find nad remove unused interactionmanagers that are not players.
        /// </summary>
        private void CleanUpUselessInteractionManagers()
        {
            List<XRInteractionManager> managers = FindObjectsOfType<XRInteractionManager>().ToList();
            managers.Remove(_xRInteractionManager);
            for (int i = 0; i < managers.Count; i++)
            {
                Destroy(managers[i].gameObject);
            }
        }

        /// <summary>
        /// Set interaction manager on all interactables
        /// </summary>
        private void SetInteractables()
        {
            XRGrabInteractable[] interactables = FindObjectsOfType<XRGrabInteractable>();
            foreach (XRGrabInteractable interactable in interactables)
            {
                interactable.interactionManager = _xRInteractionManager;
            }
        }

        /// <summary>
        /// Set interaction manager to all teleport areas
        /// </summary>
        private void SetTeleportationAreas()
        {
            TeleportationArea[] teleportationAreas = FindObjectsOfType<TeleportationArea>();
            foreach (TeleportationArea area in teleportationAreas)
            {
                area.teleportationProvider = _teleportationProvider;
                area.interactionManager = _xRInteractionManager;
            }
        }
    }
}
