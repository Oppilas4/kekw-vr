using UnityEngine;
using Kekw.Manager;

namespace Kekw.UI
{
    /// <summary>
    /// Component canbe hookes to button to request scene switch from <seealso cref="SceneSwitcher"/>
    /// </summary>
    class SceneSwitchRequest:MonoBehaviour
    {
        [SerializeField]
        [Tooltip("New scene where we should teleport")]
        string _sceneName;

        [SerializeField]
        [Tooltip("Player position in new scene")]
        Vector3 _newPlayerPosition;

        [SerializeField]
        [Tooltip("Ignore new player position in future scene")]
        bool _ignoreNewposition = false;

        /// <summary>
        /// Change scene with location setting.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="newPlayerPosition"></param>
        public void OnUiElementClicked()
        {
            SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
            if (_ignoreNewposition)
            {
                sceneSwitcher.SwitchScene(_sceneName);
            }
            else
            {
                sceneSwitcher.SetSceneJumpPosition(_newPlayerPosition);
                sceneSwitcher.SwitchScene(_sceneName, false);
            }
        }
    }
}
