using UnityEngine;

namespace Kekw.Pool
{
    /// <summary>
    /// Spawns drink on scene start.
    /// </summary>
    class DrinkSpawner: MonoBehaviour
    {
        DrinkPool _drinkPool;
        APoolMember _spawnedDrink;

        private void Start()
        {
            _drinkPool = FindObjectOfType<DrinkPool>();
            _spawnedDrink = _drinkPool.GetFromPool();
            _spawnedDrink.transform.position = this.transform.position;
            // Subscribe to event that is fired when drinks want to reset back to pool.
            DrinkSpawnManager.OnDrinkReset += DrinkSpawnManager_OnDrinkReset;
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
            }
        }
    }
}
