using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Kekw.Interaction
{

    public enum RotateArounxAxis
    {
        X,
        Y,
        Z
    }

    /// <summary>
    /// Rotates knob
    /// </summary>
    class Rotate: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Rotation speed multiplier")]
        float _speed = 1f;

        [SerializeField]
        [Tooltip("Rotation axis in local space")]
        RotateArounxAxis _around;

        InputAction _leftRotation;
        InputAction _rightRotation;

        bool _isInteracting = false;
        int _direction = 0;

        byte[] _handMask = { 0, 0 };

        private void Start()
        {
            InputActionManager inputActionManager = FindObjectOfType<InputActionManager>();

            // Controller z rotation actions
            _rightRotation = inputActionManager.actionAssets[0].FindActionMap("XRI RightHand").FindAction("Rotation");
            _leftRotation = inputActionManager.actionAssets[0].FindActionMap("XRI LeftHand").FindAction("Rotation");
        }

        private void _rotation_performed(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<Quaternion>().z;
            if (direction >= 0.15f)
            {
                _direction = -1;
            }
            else if(direction <= -.15f)
            {
                _direction = 1;
            }
        }

        private void Update()
        {
            if (_isInteracting)
            {
                switch (_around)
                {
                    case RotateArounxAxis.X:
                        this.transform.rotation = Quaternion.Euler(this.transform.localEulerAngles.x + _speed * _direction, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
                        break;
                    case RotateArounxAxis.Y:
                        this.transform.rotation = Quaternion.Euler(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y + _speed * _direction, this.transform.localEulerAngles.z);
                        break;
                    case RotateArounxAxis.Z:
                        this.transform.rotation = Quaternion.Euler(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z + _speed * _direction);
                        break;
                    default:
                        break;
                }
            }
        }

        public void OnKnobSelected()
        {
            if(_handMask[0] == 1)
            {
                _leftRotation.performed += _rotation_performed;
            }

            if(_handMask[1] == 1)
            {
                _rightRotation.performed += _rotation_performed;
            }
            _isInteracting = true;
        }

        public void OnKnobReleased()
        {
            _isInteracting = false;
            if (_handMask[0] == 1)
            {
                _leftRotation.performed -= _rotation_performed;
            }

            if (_handMask[1] == 1)
            {
                _rightRotation.performed -= _rotation_performed;
            }

            _handMask[0] = 0;
            _handMask[1] = 0;
            _direction = 0;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Set hand mask to bind correct input rotation.
            if (collision.gameObject.CompareTag("LeftHand"))
            {
                _handMask[0] = 1;
                _handMask[1] = 0;
            }
            else if (collision.gameObject.CompareTag("RightHand"))
            {
                _handMask[0] = 0;
                _handMask[1] = 1;
            }
        }
    }
}
