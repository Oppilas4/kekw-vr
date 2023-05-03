using UnityEngine;

namespace Kekw.Manager
{
    /// <summary>
    /// Manages transition to make scene.
    /// </summary>
    class MakeLoreManager: MonoBehaviour
    {
        /// <summary>
        /// Background audio source
        /// </summary>
        [SerializeField]
        [Tooltip("BGR audio source")]
        AudioSource _bgrSource;

        /// <summary>
        /// Teleport audio source
        /// </summary>
        [SerializeField]
        [Tooltip("Teleport thing to activate")]
        GameObject _teleport;

        /// <summary>
        /// What happens when lore audio ends.
        /// </summary>
        public void OnLoreEnd()
        {
            _bgrSource.Play();
            _teleport.SetActive(true);
        }
    }
}
