using UnityEngine;

namespace Kekw.Interaction.Igbuttontest
{
    /// <summary>
    /// Simple test case for ButtonTestScene.
    /// Should not be used in game.
    /// </summary>
    public class IGButtonTest : MonoBehaviour, IIngameButtonLogic
    {
        public void TriggerAction()
        {
            Debug.Log("Button pressed, KEKW!");
        }
    }
}
