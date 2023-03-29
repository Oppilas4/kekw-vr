using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Assets.Scripts.Interaction
{
    /// <summary>
    /// Deactivates hand colliders when holding item in hand.
    /// </summary>
    class HandDeActivation: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Left hand")]
        GameObject _leftHand;

        [SerializeField]
        [Tooltip("Right hand")]
        GameObject _rightHand;

        InputActionManager _inputActionManager;
        InputAction _handActivationRight;
        InputAction _handActivationLeft;

        Coroutine _releaseDelay;


        private void Awake()
        {
            // Get reference to InputActionManager
            _inputActionManager = FindObjectOfType<InputActionManager>();

            // handactivation input action
            _handActivationRight = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand Interaction").FindAction("Select");
            _handActivationLeft = _inputActionManager.actionAssets[0].FindActionMap("XRI LeftHand Interaction").FindAction("Select");

            // Attach event for hand activation input action performed.
            _handActivationRight.performed += OnItemPickedUp;
            _handActivationRight.canceled += OnItemReleased;
            _handActivationLeft.performed += OnItemPickedUp;
            _handActivationLeft.canceled += OnItemReleased;
        }

        /// <summary>
        /// What happens when player presses grip button
        /// </summary>
        /// <param name="context"></param>
        private void OnItemPickedUp(InputAction.CallbackContext context)
        {
            if(_releaseDelay != null)
            {
                StopCoroutine(_releaseDelay);
            }

            _leftHand.SetActive(false);
            _rightHand.SetActive(false);

        }

        /// <summary>
        /// What happens when player Releases grip button
        /// </summary>
        /// <param name="context"></param>
        private void OnItemReleased(InputAction.CallbackContext context)
        {
            if (_releaseDelay != null)
            {
                StopCoroutine(_releaseDelay);
            }

            _releaseDelay = StartCoroutine(ReleaseDelay());
            
        }

        IEnumerator ReleaseDelay()
        {
            yield return new WaitForSeconds(2f);
            _leftHand.SetActive(true);
            _rightHand.SetActive(true);
        }
    }
}
