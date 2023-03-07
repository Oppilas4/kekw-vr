using UnityEngine;
using Kekw.Interaction;

namespace Kekw.Interaction.Igbuttontest
{
    /// <summary>
    /// Simple test case for ButtonTestScene
    /// </summary>
    class IGButtonTest : MonoBehaviour, IIngameButtonLogic
    {
        public void TriggerAction()
        {
            Debug.Log("Button pressed, KEKW!");
        }
    }
}
