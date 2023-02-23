using UnityEngine;


namespace Kekw.Interaction
{
    /// <summary>
    /// Component sends haptic feedback when button bottom trigger is collided.
    /// </summary>
    public class ButtonTop : MonoBehaviour, IHapticFeedbackSender
    {
        // Hand that presses button
        string _handTag;

        LeftHapticBroker _leftHapticBroker;
        RightHapticBroker _rightHapticBroker;

        const float _hapticForce = .5f;
        const float _hapticDuration = .25f;

        private void Awake()
        {
            _leftHapticBroker = FindAnyObjectByType<LeftHapticBroker>();
            _rightHapticBroker = FindAnyObjectByType<RightHapticBroker>();
            _handTag = null;
        }

        /// <summary>
        /// <seealso cref="IHapticFeedbackSender"/>
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="duration"></param>
        public void SendHapticFeedback(float amplitude, float duration)
        {
            if (_handTag != null && _handTag.Equals("LeftHand"))
            {
                _leftHapticBroker.TriggerHapticFeedback(amplitude, duration);
            }

            if (_handTag != null && _handTag.Equals("RightHand"))
            {
                _rightHapticBroker.TriggerHapticFeedback(amplitude, duration);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("IngameButton"))
            {
                SendHapticFeedback(_hapticForce, _hapticDuration);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            // Hand is in contact with button top
            if(collision.gameObject.CompareTag("LeftHand") || collision.gameObject.CompareTag("RightHand"))
            {
                _handTag = collision.gameObject.tag;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            // hand contact has ended
            _handTag = null;
        }
    }
}
