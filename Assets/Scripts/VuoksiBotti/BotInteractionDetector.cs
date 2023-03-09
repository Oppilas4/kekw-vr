using UnityEngine;
using Kekw.Interaction;
using System;
using UnityEngine.InputSystem;

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


        private void OnTriggerStay(Collider other)
        {
            // Activate bot input action map when near robot.
            if (other.gameObject.CompareTag("Player"))
            {
                InputMapInitializer.NormalLeftHandActionMap.Disable();
                InputMapInitializer.BotLeftHandActionMap.Enable();
                InputMapInitializer.BotLeftHandActionMap.FindAction("BotPause").performed += SetBotToPause;
                InputMapInitializer.BotLeftHandActionMap.FindAction("BotTalk").performed += SetBotToTalk;

            }
        }

        private void OnTriggerExit(Collider other)
        {
            // DeActivate bot input action map when left robot proximity.
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player has left bot assssss");
                InputMapInitializer.BotLeftHandActionMap.FindAction("BotPause").performed -= SetBotToPause;
                InputMapInitializer.BotLeftHandActionMap.FindAction("BotTalk").performed -= SetBotToTalk;
                InputMapInitializer.NormalLeftHandActionMap.Enable();
                InputMapInitializer.BotLeftHandActionMap.Disable();
            }
        }

        private void SetBotToTalk(InputAction.CallbackContext obj)
        {
            _talk.TalkSingle();
        }

        private void SetBotToPause(InputAction.CallbackContext obj)
        {
            _talk.SetPause();
            _animationManager.SetPause();
            _boomBox.SetPause();
            _mover.SetPause();
        }

        
    }
}
