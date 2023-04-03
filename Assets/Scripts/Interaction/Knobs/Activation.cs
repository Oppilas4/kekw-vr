using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Activate something with collision.
    /// </summary>
    class Activation:MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Hidden gameobject")]
        GameObject _hidden;

        private void OnCollisionEnter(Collision collision)
        {
            if (_hidden != null && !_hidden.activeSelf)
            {
                this.GetComponent<Rigidbody>().useGravity = true;
                _hidden.SetActive(true);
            }
        }
    }
}
