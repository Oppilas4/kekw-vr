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
            }
        }

        public void SetTeleportationAreas()
        {
            TeleportationArea[] teleportationAreas = FindObjectsOfType<TeleportationArea>();
            foreach (TeleportationArea area in teleportationAreas)
            {
                area.teleportationProvider = _teleportationProvider;
            }
        }
    }
}
