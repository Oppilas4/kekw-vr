using UnityEngine;

namespace Kekw.Interaction.Test
{
    class ButtonTest : MonoBehaviour, IIngameButtonLogic
    {
        public void TriggerAction()
        {
            Debug.Log("Button works");
        }
    }
}
