using UnityEngine;

namespace Kekw.Pool.Drink
{
    /// <summary>
    /// Spawns drink from pool on scene start.
    /// </summary>
    public class DrinkSpawner : MonoBehaviour
    {
        /// <summary>
        /// Should this spawn on start?
        /// </summary>
        [SerializeField]
        [Tooltip("Spawn at start")]
        bool _spawnAtStart = true;

        /// <summary>
        /// Should this spawn new item if old is destroyed.
        /// </summary>
        [SerializeField]
        [Tooltip("Spawn new on destroy")]
        bool _spawnOnDestroy = true;

        DrinkPool _drinkPool;
        APoolMember _spawnedDrink;

        private void Start()
        {
            _drinkPool = FindObjectOfType<DrinkPool>();
            if (_spawnAtStart)
            {
                _spawnedDrink = _drinkPool.GetFromPool();
                _spawnedDrink.transform.position = this.transform.position;
            }
            if (_spawnOnDestroy)
            {
                // Subscribe to event that is fired when drinks want to reset back to pool.
                DrinkSpawnManager.OnDrinkReset += DrinkSpawnManager_OnDrinkReset;
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from event that is fired when drinks want to reset back to pool.
            DrinkSpawnManager.OnDrinkReset -= DrinkSpawnManager_OnDrinkReset;
        }

        /// <summary>
        /// Method is called when OnDrinkReset is invoked.
        /// <seealso cref="DrinkSpawnManager"/>
        /// </summary>
        private void DrinkSpawnManager_OnDrinkReset()
        {
            if (!_spawnedDrink.isActiveAndEnabled)
            {
                if (_drinkPool.GetQueueuLength() > 0)
                {
                    _spawnedDrink = _drinkPool.GetFromPool();
                    _spawnedDrink.transform.position = this.transform.position;
                }
                else
                {
                    Debug.LogWarning("Drink pool is empty");
                }
            }
        }

        /// <summary>
        /// Spawn drink manually.
        /// </summary>
        /// <returns></returns>
        public APoolMember SpawnDrink()
        {

            if (_drinkPool.GetQueueuLength() > 0)
            {
                _spawnedDrink = _drinkPool.GetFromPool();
                _spawnedDrink.transform.position = this.transform.position;
                return _spawnedDrink;
            }
            else
            {
                Debug.LogWarning("Drink pool is empty");
                return null;
            }
        }
    }
}
