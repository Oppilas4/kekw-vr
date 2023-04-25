using System;
using UnityEngine;

namespace Kekw.Interaction.Football
{
    /// <summary>
    /// Sends stick haptic when stick is touched... <--- ^_- nice
    /// </summary>
    class StickHaptic: MonoBehaviour
    {
        LeftHapticBroker _leftHapticBroker;
        RightHapticBroker _rightHapticBroker;

        private void Start()
        {
            _leftHapticBroker = FindObjectOfType<LeftHapticBroker>();
            _rightHapticBroker = FindObjectOfType<RightHapticBroker>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag("RightHand"))
            {
                _rightHapticBroker.TriggerHapticFeedback(.5f, .1f);
            }

            if (collision.collider.gameObject.CompareTag("LeftHand"))
            {
                _leftHapticBroker.TriggerHapticFeedback(.5f, .1f);
            }
        }
    }
}
