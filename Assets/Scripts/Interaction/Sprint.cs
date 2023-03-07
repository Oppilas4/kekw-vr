using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Kekw.Interaction
{
    /// <summary>
    /// Character sprint ability
    /// </summary>
    class Sprint: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Movement provider script")]
        DynamicMoveProvider _dynamicMoveProvider;

        [SerializeField]
        [Tooltip("Sprint speed")]
        float _sprintSpeed = 3f;

        float _normalSpeed;

        InputAction _sprintAction;

        float _currentSpeed;
        float _targetSpeed;

        private void Awake()
        {
            _normalSpeed = _dynamicMoveProvider.moveSpeed;
            // Find input action
            _sprintAction = FindObjectOfType<InputActionManager>().actionAssets[0].FindActionMap("XRI RightHand").FindAction("SprintActivate");
            _sprintAction.performed += SprintActionPerformed;
            _sprintAction.canceled += SprintActionPerformed;
            _currentSpeed = _normalSpeed;
            _targetSpeed = _normalSpeed;
        }

        private void Update()
        {
            // Set speed to target if delta is small enough
            if(Mathf.Abs(_currentSpeed -_targetSpeed) < .05f)
            {
                _dynamicMoveProvider.moveSpeed = _targetSpeed;
                return;
            }
            // Lerp towards _targetSpeed
            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, 5f * Time.deltaTime);
            _dynamicMoveProvider.moveSpeed = _currentSpeed;
            
        }

        /// <summary>
        /// Set current move speed based on input.
        /// </summary>
        /// <param name="context"></param>
        private void SprintActionPerformed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _targetSpeed = _sprintSpeed;
            }
            else
            {
                _targetSpeed = _normalSpeed;
            }
        }
    }
}
