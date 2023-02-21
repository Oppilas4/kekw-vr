using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;

namespace Kekw.Interaction
{
    /// <summary>
    /// Hand activation control.
    /// </summary>
    class HandActivation: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Left hand")]
        GameObject leftHand;

        [SerializeField]
        [Tooltip("Right hand")]
        GameObject RightHand;

        InputActionManager inputActionManager;
        InputAction handActivation;

        private bool handsActive;

        private void Awake()
        {
            // Get reference to InputActionManager
            inputActionManager = FindObjectOfType<InputActionManager>();

            // handactivation input action
            handActivation = inputActionManager.actionAssets[0].FindActionMap("XRI RightHand").FindAction("HandActivate");

            // Attach event for hand activation input action performed.
            handActivation.performed += OnHandActivated;
            handsActive = false;
        }

        /// <summary>
        /// Sets hands active or deactive.
        /// </summary>
        /// <param name="context"></param>
        private void OnHandActivated(InputAction.CallbackContext context)
        {
            handsActive = !handsActive;
            RightHand.SetActive(handsActive);
            leftHand.SetActive(handsActive);
        }
    }
}
