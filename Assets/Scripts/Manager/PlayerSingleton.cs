using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
                SetTeleportationAreas();
                SetInteractables();

                // TODO clean up other XRInteractionManager
                CleanUpUselessInteractionManagers();
            }
        }

        public void CleanUpUselessInteractionManagers()
        {
            Debug.Log(" TODO clean up other XRInteractionManager");
        }

        public void SetInteractables()
        {
            XRGrabInteractable[] interactables = FindObjectsOfType<XRGrabInteractable>();
            foreach (XRGrabInteractable interactable in interactables)
            {
                interactable.interactionManager = _xRInteractionManager;
            }
        }

        public void SetTeleportationAreas()
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
