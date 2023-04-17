using UnityEngine;

namespace Assets.Scripts.Manager
{
    /// <summary>
    /// Keep xr interaction manager alive. 
    /// Simple singleton does not destroy copies.
    /// </summary>
    class InteractionManagerSingleton: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
