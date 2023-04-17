using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace Kekw.Interaction
{
    /// <summary>
    /// Disables bot interaction map at the beginning of the play.
    /// </summary>
    class InputMapInitializer: MonoBehaviour
    {
        InputActionManager _inputActionManager;

        /// <summary>
        /// Default action map used for left hand.
        /// </summary>
        public static InputActionMap NormalLeftHandActionMap { get; private set; }
       
        /// <summary>
        /// Special action map that is only used near the robot.
        /// </summary>
        public static InputActionMap BotLeftHandActionMap { get; private set; }

        private void Awake()
        {
            // find input action manager and disable bot input action map.
            _inputActionManager = FindObjectOfType<InputActionManager>();
            BotLeftHandActionMap = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand_botti");
            NormalLeftHandActionMap = _inputActionManager.actionAssets[0].FindActionMap("XRI RightHand");
            BotLeftHandActionMap.Disable();
            NormalLeftHandActionMap.Enable();
        }
    }
}
