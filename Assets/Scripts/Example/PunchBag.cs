using UnityEngine;
using Kekw.Interaction;

namespace Kekw.Example
{
    /// <summary>
    /// Punchbag haptic example.
    /// </summary>
    public class PunchBag: MonoBehaviour
    {
        LeftHapticBroker _leftHapticBroker;
        RightHapticBroker _rightHapticBroker;


        private void Awake()
        {
            // Find broker objects
            _leftHapticBroker = FindObjectOfType<LeftHapticBroker>();
            _rightHapticBroker = FindObjectOfType<RightHapticBroker>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("LeftHand"))
            {
                _leftHapticBroker.TriggerHapticFeedback(1f, .1f);
            }

            if (collision.gameObject.CompareTag("RightHand"))
            {
                _rightHapticBroker.TriggerHapticFeedback(1f, .1f);
            }
        }
    }
}
