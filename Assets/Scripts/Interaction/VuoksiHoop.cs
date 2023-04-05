
using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Vuoksi hoop ball detector
    /// </summary>
    class VuoksiHoop: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Goal sound")]
        AudioSource _goalSound;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                _goalSound.PlayOneShot(_goalSound.clip);
            }
        }
    }
}
