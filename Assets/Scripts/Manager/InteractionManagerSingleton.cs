using UnityEngine;

namespace Kekw.Manager
{
    /// <summary>
    /// Keep xr interaction manager alive. 
    /// Simple singleton does not destroy copies.
    /// </summary>
    public class InteractionManagerSingleton : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
