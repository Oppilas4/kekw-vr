using UnityEngine;

namespace Kekw.Manager
{
    /// <summary>
    /// Survives scene changes.
    /// </summary>
    class NonDestructable: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
