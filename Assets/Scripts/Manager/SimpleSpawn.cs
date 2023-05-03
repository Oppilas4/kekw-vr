using UnityEngine;

namespace Kekw.Manager
{
    /// <summary>
    /// Instantiates given object at awake or via Spawn method.
    /// </summary>
    class SimpleSpawn: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Object to spawn")]
        GameObject _spawnPrefab;

        private void Awake()
        {
            Spawn();
        }

        /// <summary>
        /// Spawn object manually.
        /// </summary>
        public void Spawn()
        {
            GameObject spawned = Instantiate(_spawnPrefab, this.transform.position, Quaternion.identity);
            spawned.GetComponentInChildren<ISpawnAble>().SetSpawner(this);
        }
    }
}
