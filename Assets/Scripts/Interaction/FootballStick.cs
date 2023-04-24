using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Interaction
{
    public enum StickControlType
    {
        ROTATOR,
        MOVER
    }

    /// <summary>
    /// Football stick behaviour
    /// Controls physical stick 
    /// </summary>
    class FootballStick: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Stick under control")]
        Rigidbody _rigidbody;

        [SerializeField]
        [Tooltip("movement this controls")]
        StickControlType _stickControlType;

        [SerializeField]
        [Tooltip("Emitted force")]
        Vector3 _forceEmited;

        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("RightHand") || other.gameObject.CompareTag("LeftHand"))
            {
                if (_stickControlType == StickControlType.MOVER)
                {
                    _rigidbody.AddForce(_forceEmited, ForceMode.Impulse);
                }
                if (_stickControlType == StickControlType.ROTATOR)
                {
                    _rigidbody.AddTorque(_forceEmited, ForceMode.Impulse);
                }
            }
        }
    }
}
