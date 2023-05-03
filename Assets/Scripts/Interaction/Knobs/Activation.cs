using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Activates not active gameobject when collision enters.
    /// </summary>
    public class Activation:MonoBehaviour
    {
        /// <summary>
        /// Gameobject or hirachy that is hidden.
        /// </summary>
        [SerializeField]
        [Tooltip("Hidden gameobject")]
        GameObject _hidden;

        /// <summary>
        /// Decal to show when revealing. OPTIONAL.
        /// </summary>
        [SerializeField]
        [Tooltip("Clue decal")]
        GameObject _clueDecal;

        private void OnCollisionEnter(Collision collision)
        {
            if (_hidden != null && !_hidden.activeSelf)
            {
                this.GetComponent<Rigidbody>().useGravity = true;
                _hidden.SetActive(true);
                if(_clueDecal != null)
                {
                    _clueDecal.SetActive(true);
                }   
            }
        }
    }
}
