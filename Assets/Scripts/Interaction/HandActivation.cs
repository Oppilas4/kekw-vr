using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;
using System;

namespace Kekw.Interaction
{
    /// <summary>
    /// Hand activation control. Provides event where state changes can be listened.
    /// </summary>
    [Obsolete("Class is deprecated, hand colliders are active all the time.", true)]
    public class HandActivation : MonoBehaviour
    {
        /// <summary>
        /// Event type
        /// </summary>
        /// <param name="handsActiveState"></param>
        public delegate void HandActiveAction(bool handsActiveState);

        /// <summary>
        /// Event that notifies listeners about state change.
        /// </summary>
        public static event HandActiveAction OnHandStateChanged;

        [SerializeField]
        [Tooltip("Left hand")]
        GameObject leftHand;

        [SerializeField]
        [Tooltip("Right hand")]
        GameObject RightHand;

        InputActionManager _inputActionManager;
        InputAction _handActivation;

        private bool _handsActive;

        private void Awake()
        {
            // Get reference to InputActionManager
            _inputActionManager = FindObjectOfType<InputActionManager>();

            // handactivation input action
            _handActivation = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand").FindAction("HandActivate");

            // Attach event for hand activation input action performed.
            _handActivation.performed += OnHandActiveStatusChange;
            _handsActive = false;
        }

        /// <summary>
        /// Sets hands active or deactive.
        /// </summary>
        /// <param name="context"></param>
        [Obsolete("Input binding is deprecated", true)]
        private void OnHandActiveStatusChange(InputAction.CallbackContext context)
        {
            _handsActive = !_handsActive;
            RightHand.SetActive(_handsActive);
            leftHand.SetActive(_handsActive);
            OnHandStateChanged?.Invoke(_handsActive);
        }
    }
}
