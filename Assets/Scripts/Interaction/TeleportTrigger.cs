using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Detecs player entering to teleport area.
    /// </summary>
    class TeleportTrigger: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Gameobject to activate when player enters the trigger.")]
        GameObject _target;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                _target.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _target.SetActive(false);
            }
        }
    }
}
