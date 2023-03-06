using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;
using Kekw.Interaction;

namespace Kekw.Animation
{
    /// <summary>
    /// Hand identification
    /// </summary>
    enum Hand
    {
        Left,
        Right
    }

    /// <summary>
    /// Handles hand animation playing in sync with user interaction mode selection.
    /// </summary>
    public class HandAnimation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Hand color when in 'punch mode'")]
        Color _color;

        [SerializeField]
        [Tooltip("Which hand this is")]
        Hand hand;

        Animator _animator;
        Material _material;
        Color _originalColor;
        InputActionManager _inputActionManager;

        // Item in hand actions
        InputAction _holdItemActionL;
        InputAction _holdItemActionR;

        // Teleport mode actions
        InputAction _teleportModeActionLeft;
        InputAction _teleportModeActionRight;
        InputAction _teleportModeActionLeftCancelled;
        InputAction _teleportModeActionRightCancelled;


        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _material = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
            _originalColor = _material.color;
            _inputActionManager = FindObjectOfType<InputActionManager>();
            switch (hand)
            {
                case Hand.Left:
                    // Find item hold action
                    _holdItemActionL = _inputActionManager.actionAssets[0].FindActionMap("XRI LeftHand Interaction").FindAction("Select");
                    // Bind grip trigger pressed
                    _holdItemActionL.performed += OnItemHeld;
                    //Bind grip trigger cancelled
                    _holdItemActionL.canceled += OnItemReleased;
                    // Find teleport activate actions
                    _teleportModeActionLeft = _inputActionManager.actionAssets[0].FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
                    _teleportModeActionLeft.performed += TeleportModeActionPerformed;
                    _teleportModeActionLeft.canceled += TeleportModeActionPerformed;
                    // find and bind teleport canceled actions
                    _teleportModeActionLeftCancelled = _inputActionManager.actionAssets[0].FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
                    _teleportModeActionLeftCancelled.performed += TeleportModeActionCanceled;
                    break;
                case Hand.Right:
                    // Find item hold action
                    _holdItemActionR = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand Interaction").FindAction("Select");
                    // Bind grip trigger pressed
                    _holdItemActionR.performed += OnItemHeld;
                    //Bind grip trigger cancelled
                    _holdItemActionR.canceled += OnItemReleased;
                    // Find teleport activate actions
                    _teleportModeActionRight = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Activate");
                    _teleportModeActionRight.performed += TeleportModeActionPerformed;
                    _teleportModeActionRight.canceled += TeleportModeActionPerformed;
                    // find and bind teleport canceled actions
                    _teleportModeActionRightCancelled = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Cancel");
                    _teleportModeActionRightCancelled.performed += TeleportModeActionCanceled;
                    break;
                default:
                    throw new System.Exception("Hand is not selected!");
            }

            // Bind to event hand active state changed.
            HandActivation.OnHandStateChanged += ToggleColor;

            /*
            // Find item hold action
            _holdItemActionL = _inputActionManager.actionAssets[0].FindActionMap("XRI LeftHand Interaction").FindAction("Select");
            _holdItemActionR = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand Interaction").FindAction("Select");
            // Bind grip trigger pressed
            _holdItemActionL.performed += OnItemHeld;
            _holdItemActionR.performed += OnItemHeld;
            //Bind grip trigger cancelled
            _holdItemActionL.canceled += OnItemReleased;
            _holdItemActionR.canceled += OnItemReleased;
            // Bind to event hand active state changed.
            HandActivation.OnHandStateChanged += ToggleColor;

            // Find teleport activate actions
            _teleportModeActionLeft = _inputActionManager.actionAssets[0].FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
            _teleportModeActionRight = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Activate");
            _teleportModeActionLeft.performed += TeleportModeActionPerformed;
            _teleportModeActionRight.performed += TeleportModeActionPerformed;
            // find and bind teleport canceled actions
            _teleportModeActionLeftCancelled = _inputActionManager.actionAssets[0].FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
            _teleportModeActionRightCancelled = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Cancel");
            _teleportModeActionLeftCancelled.performed += TeleportModeActionCanceled;
            _teleportModeActionRightCancelled.performed += TeleportModeActionCanceled;*/
        }

        /// <summary>
        /// Cancel teleport action.
        /// </summary>
        /// <param name="obj"></param>
        private void TeleportModeActionCanceled(InputAction.CallbackContext context)
        {
            if (_animator.GetBool("Teleport"))
            {
                _animator.SetBool("Teleport", false);
            }
        }

        /// <summary>
        /// Play hand animation when entered to teleport mode.
        /// </summary>
        /// <param name="obj"></param>
        private void TeleportModeActionPerformed(InputAction.CallbackContext context)
        {
            // Use default .5f dead zone
            if(context.performed && context.ReadValue<Vector2>().y > .5f)
            {
                if(_animator.GetBool("Teleport") == false)
                {
                    _animator.SetBool("Teleport", true);
                }
            }
            else 
            {
                // under deadzone => teleport is not active
                this.TeleportModeActionCanceled(new InputAction.CallbackContext());
            }
        }

        /// <summary>
        /// Callback for event defined in <seealso cref="HandActivation"/>
        /// </summary>
        /// <param name="handsActiveState"></param>
        private void ToggleColor(bool handsActiveState)
        {
            // If hands are active play punch pose and change color.
            if (handsActiveState)
            {
                _material.color = _color;
                if (!_animator.GetBool("Punch"))
                {
                    _animator.SetBool("Punch", true);
                }
            }
            // If hands are not active play idle animation and change color back to gray.
            else
            {
                _material.color = _originalColor;
                if (_animator.GetBool("Punch"))
                {
                    _animator.SetBool("Punch", false);
                }
            }
        }

        /// <summary>
        /// Called from grip button performed
        /// </summary>
        /// <param name="context"></param>
        private void OnItemReleased(InputAction.CallbackContext context)
        {
            // Check that animation boolean is not already set to false
            if (_animator.GetBool("PickUp"))
            {
                _animator.SetBool("PickUp", false);
            }
        }

        /// <summary>
        /// Called from grip button performed
        /// </summary>
        /// <param name="context"></param>
        private void OnItemHeld(InputAction.CallbackContext context)
        {
            // Check that animation boolean is not already set to true
            if (_animator.GetBool("PickUp") != true)
            {
                _animator.SetBool("PickUp", true);
            }
        }
    }
}