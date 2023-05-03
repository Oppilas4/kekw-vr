using UnityEngine;

namespace Kekw.Pool.Drink
{
    /// <summary>
    /// Provides event for drink spawning.
    /// </summary>
    class DrinkSpawnManager: MonoBehaviour
    {
        /// <summary>
        /// Event type
        /// </summary>
        /// <param name="handsActiveState"></param>
        public delegate void DrinkStateChange();

        /// <summary>
        /// Event that notifies listeners about state change.
        /// </summary>
        public static event DrinkStateChange OnDrinkReset;

        /// <summary>
        /// Drinks raise this event when they want to return to pool.
        /// </summary>
        public static void RaiseEvent()
        {
            OnDrinkReset?.Invoke();
        }
    }
}
