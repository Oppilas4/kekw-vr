using UnityEngine;
using Kekw.Interaction;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Kekw.VuoksiBotti
{
    /// <summary>
    /// Handles input action switching and bot human interface.
    /// </summary>
    class BotInteractionDetector: MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Boombox")]
        BoomBox _boomBox;

        [SerializeField]
        [Tooltip("Mover component")]
        Mover _mover;

        [SerializeField]
        [Tooltip("Robot animation manager")]
        AnimationManager _animationManager;

        [SerializeField]
        [Tooltip("Talk component")]
        Talk _talk;

        [SerializeField]
        [Tooltip("Ui panel to display bot buttons.")]
        GameObject _botHelpUi;


        private void OnTriggerStay(Collider other)
        {
            // Activate bot input action map when near robot.
            if (other.gameObject.CompareTag("Player"))
            {
                _botHelpUi.SetActive(true);
                InputMapInitializer.NormalLeftHandActionMap.Disable();
                InputMapInitializer.BotLeftHandActionMap.Enable();
                InputMapInitializer.BotLeftHandActionMap.FindAction("BotPause").performed += SetBotToPause;
                InputMapInitializer.BotLeftHandActionMap.FindAction("BotTalk").performed += SetBotToTalk;

            }
        }

        private void OnDestroy()
        {
            DeactivateBotInteraction();
        }

        private void OnTriggerExit(Collider other)
        {
            // DeActivate bot input action map when left robot proximity.
            if (other.gameObject.CompareTag("Player"))
            {
                DeactivateBotInteraction();
            }
        }

        /// <summary>
        /// Deactivates bot interaction
        /// </summary>
        public void DeactivateBotInteraction()
        {
            _botHelpUi.SetActive(false);
            InputMapInitializer.BotLeftHandActionMap.FindAction("BotPause").performed -= SetBotToPause;
            InputMapInitializer.BotLeftHandActionMap.FindAction("BotTalk").performed -= SetBotToTalk;
            InputMapInitializer.BotLeftHandActionMap.Disable();
            InputMapInitializer.NormalLeftHandActionMap.Enable();
        }

        private void SetBotToTalk(InputAction.CallbackContext obj)
        {
            _talk.TalkSingle();
        }

        private void SetBotToPause(InputAction.CallbackContext obj)
        {
            if (!_animationManager.IsTalking)
            {
                _talk.SetPause();
                _animationManager.SetPause();
                _boomBox.SetPause();
                _mover.SetPause();
            }
        }
    }
}
