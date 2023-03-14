using UnityEngine;

namespace Kekw.Manager
{
    class NonDestructable: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
