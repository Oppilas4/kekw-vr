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
        [SerializeField]
        [Tooltip("Rotation speed multiplier")]
        float _speed = 1f;


        InputAction _leftRotation;
        InputAction _rightRotation;

        bool _isInteracting = false;
        int _direction = 0;

        private void Start()
        {
            InputActionManager inputActionManager = FindObjectOfType<InputActionManager>();

            // Controller z rotation actions
            _rightRotation = inputActionManager.actionAssets[0].FindActionMap("XRI RightHand").FindAction("Rotation");
            _leftRotation = inputActionManager.actionAssets[0].FindActionMap("XRI RightHand").FindAction("Rotation");
        }

        private void _rotation_performed(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<Quaternion>().z;
            if (direction >= 0)
            {
                _direction = 1;
            }
            else
            {
                _direction = -1;
            }
        }

        private void Update()
        {
            if (_isInteracting)
            {
                this.transform.Rotate(this.transform.forward, _direction * _speed);
            }
        }

        public void OnKnobSelected()
        {
            Debug.Log("Knob is in hand");
            _rightRotation.performed += _rotation_performed;
            _leftRotation.performed += _rotation_performed;
            _isInteracting = true;
        }

        public void OnKnobReleased()
        {
            Debug.Log("Knob is released from hand");
            _isInteracting = false;
            _rightRotation.performed -= _rotation_performed;
            _leftRotation.performed -= _rotation_performed;
            _direction = 0;
        }
    }
}
