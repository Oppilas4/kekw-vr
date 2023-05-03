using UnityEngine;

namespace Kekw.Interaction.Football
{
    /// <summary>
    /// Sends stick haptic when stick is touched... <--- ^_- nice<br></br><br></br>
    /// </summary>
    public class StickHaptic: MonoBehaviour
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
                _rightHapticBroker.TriggerHapticFeedback(.3f, .1f);
            }

            if (collision.collider.gameObject.CompareTag("LeftHand"))
            {
                _leftHapticBroker.TriggerHapticFeedback(.3f, .1f);
            }
        }
    }
}
