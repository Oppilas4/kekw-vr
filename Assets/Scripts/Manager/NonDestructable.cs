using UnityEngine;

namespace Kekw.Manager
{
    /// <summary>
    /// Survives scene changes.
    /// </summary>
    public class NonDestructable : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
