using UnityEngine;

namespace Kekw.Manager
{
    class MakeLoreManager: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("BGR audio source")]
        AudioSource _bgrSource;

        [SerializeField]
        [Tooltip("Teleport thing to activate")]
        GameObject _teleport;


        public void OnLoreEnd()
        {
            _bgrSource.Play();
            _teleport.SetActive(true);
        }
    }
}
