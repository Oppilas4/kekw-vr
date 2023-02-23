using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Kekw.Interaction
{
    /// <summary>
    /// Abstract base class, transmits haptic request to XrController component.
    /// Sub classes attach to same component as XRController that we want to control.
    /// </summary>
    [RequireComponent(typeof(ActionBasedController))]
    public abstract class AHapticBroker : MonoBehaviour
    {
        ActionBasedController _xrControllerBase;

        private void Awake()
        {
            _xrControllerBase = GetComponent<ActionBasedController>();
        }

        /// <summary>
        /// Trigger haptic feedback on controller.
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="duration"></param>
        public void TriggerHapticFeedback(float amplitude, float duration) => _xrControllerBase?.SendHapticImpulse(amplitude, duration);
    } 
}
