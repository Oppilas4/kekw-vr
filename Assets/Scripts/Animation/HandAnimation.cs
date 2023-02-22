using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;
using Kekw.Interaction;
using System;

namespace Kekw.Animation
{
    /// <summary>
    /// Handles hand animation playing in sync with user interaction.
    /// </summary>
    public class HandAnimation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Hand color when in 'punch mode'")]
        Color _color;

        Animator _animator;
        Material _material;
        Color _originalColor;
        InputActionManager _inputActionManager;

        InputAction _holdItemActionL;
        InputAction _holdItemActionR;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _material = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
            _originalColor = _material.color;
            _inputActionManager = FindObjectOfType<InputActionManager>();
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
        }

        /// <summary>
        /// Callback for event defined in <seealso cref="HandActivation"/>
        /// </summary>
        /// <param name="handsActiveState"></param>
        private void ToggleColor(bool handsActiveState)
        {
            Debug.Log("sadasd");
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