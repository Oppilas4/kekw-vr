using UnityEngine;

namespace Kekw.Interaction
{
    class Destroyer: MonoBehaviour
    {
        private bool _isDestroyed = false;

        private void OnCollisionEnter(Collision collision)
        {
            if(!_isDestroyed && (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("KillPlane")))
            {
                _isDestroyed = true;
                GetComponentInChildren<IDestroyable>().OnDestroyRequested();
            }
        }
    }
}
