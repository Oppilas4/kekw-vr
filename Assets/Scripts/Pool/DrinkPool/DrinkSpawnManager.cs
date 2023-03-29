using System;
using UnityEngine;

namespace Kekw.Pool
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
        /// Raise drink state change events.
        /// </summary>
        public static void RaiseEvent()
        {
            OnDrinkReset?.Invoke();
        }

    }
}
