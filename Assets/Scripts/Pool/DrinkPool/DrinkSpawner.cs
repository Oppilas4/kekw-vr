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
            DrinkSpawnManager.OnDrinkReset += DrinkSpawnManager_OnDrinkReset;
        }

        private void OnDestroy()
        {
            DrinkSpawnManager.OnDrinkReset -= DrinkSpawnManager_OnDrinkReset;
        }

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
