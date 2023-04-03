using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Kekw.Interaction
{
    /// <summary>
    /// Rotates knob
    /// </summary>
    class Rotate: MonoBehaviour
    {
        InputAction _leftRotation;
        InputAction _rightRotation;

        bool _isInteracting = false;
        int _direction = 0;

        private void Start()
        {
            InputActionManager inputActionManager = FindObjectOfType<InputActionManager>();

            // Controller z rotation actions
            _rightRotation = inputActionManager.actionAssets[0].FindActionMap("XRI RightHand").FindAction("Rotation");
            _rightRotation.performed += _rotation_performed;
            _leftRotation = inputActionManager.actionAssets[0].FindActionMap("XRI RightHand").FindAction("Rotation");
            _leftRotation.performed += _rotation_performed;
        }

        private void _rotation_performed(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<Quaternion>());
        }

        private void Update()
        {
            if (_isInteracting)
            {

            }
        }

        public void OnKnobSelected()
        {
            Debug.Log("Knob is in hand");
        }

        public void OnKnobReleased()
        {
            Debug.Log("Knob is released from hand");
        }
    }
}
