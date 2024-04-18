using Kekw.Interaction;
using Kekw.VuoksiBotti;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MC_BotInteractionDetector : MonoBehaviour
{
    /// <summary>
    /// Bots mover
    /// </summary>
    [SerializeField]
    [Tooltip("Mover component")]
    MC_TutorialBotMover _mover;

    /// <summary>
    /// Bots talk component
    /// </summary>
    [SerializeField]
    [Tooltip("Talk component")]
    MC_BotTalk _talk;

    /// <summary>
    /// Button ui
    /// </summary>
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
       // _talk.SetPause();            
        _mover.SetPause();
    }
}
