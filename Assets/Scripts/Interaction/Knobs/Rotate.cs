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

        [SerializeField]
        [Tooltip("Rotating source")]
        AudioSource _rotatingAudio;

        InputAction _rightRotation;

        bool _isInteracting = false;
        int _direction = 0;


        private void Start()
        {
            InputActionManager inputActionManager = FindObjectOfType<InputActionManager>();

            // Controller z rotation actions
            _rightRotation = inputActionManager.actionAssets[0].FindActionMap("XRI RightHand").FindAction("Rotation");
        }

        private void _rotation_performed(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<Quaternion>().z;
            if (direction >= 0f)
            {
                _direction = -1;
            }
            else if(direction < 0f)
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

        /// <summary>
        /// Called from xr grab interactable when pick up is pressed
        /// </summary>
        public void OnKnobSelected()
        {
            _rightRotation.performed += _rotation_performed;
            _isInteracting = true;
            _rotatingAudio.Play();
        }

        /// <summary>
        /// Called from xr grab interactable when pick up is pressed
        /// </summary>
        public void OnKnobReleased()
        {
            _rotatingAudio.Stop();
            _isInteracting = false;
            _rightRotation.performed -= _rotation_performed;
            _direction = 0;
        }
    }
}
