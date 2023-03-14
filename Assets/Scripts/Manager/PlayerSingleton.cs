using UnityEngine;

namespace Kekw.Manager
{
    /// <summary>
    /// Simple singleton that survives scene changes.
    /// </summary>
    class PlayerSingleton: MonoBehaviour
    {
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
            }
        }
    }
}
