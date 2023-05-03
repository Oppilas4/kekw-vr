using UnityEngine;

namespace Kekw.Manager
{
    /// <summary>
    /// Spawn point for player.
    /// </summary>
    class PlayerSpawn: MonoBehaviour
    {
        /// <summary>
        /// Player prefab to spawn.
        /// </summary>
        [SerializeField]
        [Tooltip("PLayer prefab")]
        GameObject _playerPrefab;

        private void Awake()
        {
            // Spawn player if static reference is null.
            if(PlayerSingleton.Instance == null)
            {
                GameObject temp = Instantiate(_playerPrefab);
                temp.transform.position = this.transform.position;
                temp.transform.rotation = this.transform.rotation;
            }
            else
            {
                // Reset player position to spawn point.
                PlayerSingleton.Instance.gameObject.transform.position = this.transform.position;
            }
        }
    }
}
