using UnityEngine;

namespace Kekw.Manager
{
    /// <summary>
    /// Keep xr interaction manager alive. 
    /// Simple singleton does destroy duplicate instances.
    /// </summary>
    public class InteractionManagerSingleton : MonoBehaviour
    {
        // Singleton instance
        private static InteractionManagerSingleton _Instance;

        private void Awake()
        {
            if(_Instance != null && _Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
