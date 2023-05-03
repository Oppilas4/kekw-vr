using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Destroy game objects if they hit fool or killplane.
    /// Detection happens with tags 'Floor' and 'KillPlane'.<br><br></br></br>
    /// This gameobject should also contain script that implements <seealso cref="IDestroyable"/> interface that handles actual destroying.
    /// </summary>
    public class Destroyer : MonoBehaviour
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
